import * as signalR from '@microsoft/signalr'

let notificationHub = null

const startNotificationService = async () => {
    if (!notificationHub) {
        notificationHub = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:7029/hubs/notification')
            .withAutomaticReconnect()
            .configureLogging(signalR.LogLevel.Information)
            .build()
        notificationHub.onclose(() => setTimeout(startNotificationService, 5000))
    }
    try {
        await notificationHub.start()
        console.log('NotificationHub Connected')
    } catch (err) {
        console.error('Error starting Notification SignalR:', err)
        setTimeout(startNotificationService, 5000)
    }
}

const onNotificationReceived = (callback) => {
    notificationHub?.on('ReceiveNotification', callback)
}

const sendNotification = (userId, notification) => {
    notificationHub?.invoke('SendNotification', userId, notification).catch(console.error)
}

export { startNotificationService, onNotificationReceived, sendNotification }
