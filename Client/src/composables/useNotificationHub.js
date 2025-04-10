import { onBeforeUnmount, ref } from 'vue'
import { HubConnectionBuilder, HubConnectionState, LogLevel } from '@microsoft/signalr'
import { logger } from '@/utils/logger'
import useCache from '@/utils/cache/useCache'
import { CACHE_KEYS } from '@/utils/cache/cacheConstants'

const MAX_RETRY_COUNT = 10 // Show reload dialog after this many retries
const connectionState = {
    instance: null,
    refCount: 0,
    listeners: new Map(), // Map<Function, number> to track callback reference counts
    retryCount: 0,
}

const isConnected = ref(false)
const showReloadDialog = ref(false)

const handleConnectionError = () => {
    if (connectionState.retryCount >= MAX_RETRY_COUNT) {
        showReloadDialog.value = true
    }
}

const initConnection = async () => {
    if (connectionState.instance?.state === HubConnectionState.Connected) {
        logger.debug('Connection already established')
        return
    }

    try {
        const token = useCache().getCache(CACHE_KEYS.ACCESS_TOKEN)
        if (!token) {
            throw new Error('No access token available')
        }

        connectionState.instance = new HubConnectionBuilder()
            .withUrl('https://localhost:7029/hubs/notifications', {
                accessTokenFactory: () => token,
            })
            .withAutomaticReconnect([0, 2000, 5000, 10000, 20000]) // Specific retry intervals
            .configureLogging(LogLevel.Debug) // Enable detailed logging
            .build()

        // Connection lifecycle events
        connectionState.instance.onreconnecting((error) => {
            connectionState.retryCount++
            logger.warn(`Attempting to reconnect (attempt ${connectionState.retryCount}):`, error)
            isConnected.value = false
            handleConnectionError()
        })

        connectionState.instance.onreconnected(() => {
            logger.info('Successfully reconnected')
            isConnected.value = true
            connectionState.retryCount = 0
            showReloadDialog.value = false
        })

        connectionState.instance.onclose((error) => {
            logger.error('Connection closed:', error)
            isConnected.value = false
            handleConnectionError()
        })

        // Add notification handler
        connectionState.instance.on('ReceiveNotification', (notification) => {
            connectionState.listeners.forEach((refCount, callback) => {
                try {
                    callback(notification)
                } catch (error) {
                    logger.error('Error in notification callback:', error)
                }
            })
        })

        await connectionState.instance.start()
        isConnected.value = true
        logger.info('SignalR Connected')
    } catch (error) {
        logger.error('SignalR Connection Error:', error)
        isConnected.value = false
        throw error
    }
}

const ensureConnection = async () => {
    if (!connectionState.instance || connectionState.instance.state === 'Disconnected') {
        await initConnection()
    }
    connectionState.refCount++
    return connectionState.instance
}

const releaseConnection = async () => {
    connectionState.refCount--

    if (connectionState.refCount === 0 && connectionState.instance) {
        try {
            await connectionState.instance.stop()
            connectionState.instance = null
            isConnected.value = false
            logger.info('SignalR Disconnected - No active subscribers')
        } catch (error) {
            logger.error('Error disconnecting SignalR:', error)
        }
    }
}

const addListener = (callback) => {
    const currentCount = connectionState.listeners.get(callback) || 0
    connectionState.listeners.set(callback, currentCount + 1)
}

const removeListener = (callback) => {
    const currentCount = connectionState.listeners.get(callback)
    if (currentCount === 1) {
        connectionState.listeners.delete(callback)
    } else if (currentCount > 1) {
        connectionState.listeners.set(callback, currentCount - 1)
    }
}

export default function useNotificationHub() {
    const subscribe = async (callback) => {
        if (typeof callback !== 'function') {
            throw new Error('Callback must be a function')
        }

        await ensureConnection()
        addListener(callback)

        return () => {
            removeListener(callback)
            releaseConnection()
        }
    }

    onBeforeUnmount(() => {
        if (connectionState.instance) {
            releaseConnection()
        }
    })

    return {
        subscribe,
        isConnected,
        connection: connectionState.instance,
        reconnect: initConnection, // Expose reconnect functionality
        showReloadDialog, // Expose reload dialog state
    }
}
