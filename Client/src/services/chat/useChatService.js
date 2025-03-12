import * as signalR from '@microsoft/signalr'

let chatHub = null

const startChatService = async () => {
    // if (!chatHub) {
    //     chatHub = new signalR.HubConnectionBuilder()
    //         .withUrl('http://localhost:5000/hubs/chat')
    //         .withAutomaticReconnect()
    //         .configureLogging(signalR.LogLevel.Information)
    //         .build()
    //     chatHub.onclose(() => setTimeout(startChatService, 5000))
    // }
    // try {
    //     await chatHub.start()
    //     console.log('ChatHub Connected')
    // } catch (err) {
    //     console.error('Error starting Chat SignalR:', err)
    //     setTimeout(startChatService, 5000)
    // }
}

const onChatMessageReceived = (callback) => {
    chatHub?.on('ReceiveMessage', callback)
}

const sendChatMessage = (receiverId, message) => {
    chatHub?.invoke('SendMessage', receiverId, message).catch(console.error)
}

export { startChatService, onChatMessageReceived, sendChatMessage }
