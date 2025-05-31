import useNotificationHub from '@/composables/useNotificationHub'
import useNotificationService from '@/services/notifications/notificationService'
import { logger } from '@/utils/logger'
import { attempt } from 'lodash'
import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useNotificationStore = defineStore('notification', () => {
    const notifications = ref([])
    const unreadCount = ref(0)
    const cursor = ref(null)
    const hasNextPage = ref(true)
    const loading = ref(false)
    const error = ref('')

    const notificationService = useNotificationService()
    const notificationHub = useNotificationHub()

    const refreshUnreadCount = async () => {
        try {
            const response = await notificationService.getUnreadCount()
            if (response) {
                unreadCount.value = response.content
            }
        } catch (error) {
            logger.error('Failed to get unread count', { error })
        }
    }

    // Add a new notification
    const addNotification = (notification) => {
        notifications.value.unshift(notification)
    }

    // Remove a notification
    const removeNotification = async (notificationId) => {
        try {
            const response = await notificationService.delete(notificationId)
            if (response) {
                notifications.value = notifications.value.filter((n) => n.id !== notificationId)
            }
        } catch (error) {
            logger.error('Failed to remove notification', { error })
            throw error
        }
    }

    // Mark a notification as read
    const markAsRead = async (notificationId) => {
        try {
            const response = await notificationService.markAsRead(notificationId)
            if (response) {
                const notification = notifications.value.find((n) => n.id === notificationId)
                if (notification) {
                    notification.isRead = true
                }
            }
        } catch (error) {
            logger.error('Failed to mark notification as read', { error })
            throw error
        }
    }

    // Mark all notifications as read
    const markAllAsRead = async () => {
        try {
            const response = await notificationService.markAllAsRead()
            if (response) {
                notifications.value.forEach((notification) => {
                    notification.isRead = true
                })
            }
        } catch (error) {
            logger.error('Failed to mark all notifications as read', { error })
            throw error
        }
    }

    // Clear all notifications
    const clearAll = async () => {
        try {
            const response = await notificationService.clearAll()
            if (response) {
                notifications.value = []
            }
        } catch (error) {
            logger.error('Failed to clear notifications', { error })
            throw error
        }
    }

    // Load more notifications with pagination
    const loadMore = async () => {
        loading.value = true
        error.value = ''

        try {
            const response = await notificationService.getPaginatedNotifications({
                cursor: cursor.value,
                limit: 5,
            })

            if (response?.content) {
                const { items, cursor: nextCursor, hasNextPage: more } = response.content
                notifications.value = [...notifications.value, ...(items ?? [])]
                cursor.value = nextCursor
                hasNextPage.value = more
            }
        } catch (err) {
            error.value = 'Failed to load notifications'
            logger.error('Failed to load notifications', { error: err })
            throw err
        } finally {
            loading.value = false
        }
    }

    const resetState = async () => {
        notifications.value = []
        unreadCount.value = 0
        cursor.value = null
        hasNextPage.value = true
        error.value = ''
        loading.value = false

        // Dispose notification hub connection
        await notificationHub.dispose()
    }

    const initializeNotificationService = async (
        initOptions = { onNotificationReceived: null },
    ) => {
        try {
            await Promise.all([
                refreshUnreadCount(),
                // Set up real-time notification listener
                notificationHub.subscribe((notification) => {
                    addNotification(notification)
                    refreshUnreadCount()
                    attempt(initOptions?.onNotificationReceived, notification)
                }),
            ])
        } catch (error) {
            logger.error('Error initializing notification service:', error)
            throw error
        }
    }

    // Keep existing data and only load if empty
    const keepLoadedData = async () => {
        try {
            // Only load data if empty, as hub is already initialized
            if (notifications.value.length === 0) {
                await loadMore()
            }
            await refreshUnreadCount()
        } catch (error) {
            logger.error('Error keeping loaded data:', error)
            throw error
        }
    }

    return {
        // State
        notifications,
        unreadCount,
        loading,
        error,
        hasNextPage,

        // Actions
        addNotification,
        removeNotification,
        markAsRead,
        markAllAsRead,
        clearAll,
        loadMore,
        refreshUnreadCount,
        resetState,
        keepLoadedData,
        initializeNotificationService,
    }
})
