import { NotificationType } from '@/common/enums/notificationType'
import FriendRequestActions from './FriendRequestActions.vue'

// Map notification types to their action components
export const NOTIFICATION_ACTIONS = {
    [NotificationType.FRIEND_REQUEST]: FriendRequestActions,
}

/**
 * Get the appropriate action component for a notification type
 * @param {number} type - The notification type enum value
 * @returns {Component|null} - The Vue component for handling the notification actions, or null if no actions
 */
export const getNotificationActions = (type) => {
    return NOTIFICATION_ACTIONS[type] || null
}
