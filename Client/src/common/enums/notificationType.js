/**
 * Enum for notification types starting from 1 to match server implementation
 * @enum {number}
 */
export const NotificationType = {
    FRIEND_REQUEST: 1,
    POST_LIKE: 2,
    POST_COMMENT: 3,
    COMMENT_REPLY: 4,
}

/**
 * Convert notification type number to string representation
 * @param {number} type - The notification type enum value
 * @returns {string} The string representation of the notification type
 */
export const getNotificationTypeString = (type) => {
    return Object.keys(NotificationType).find((key) => NotificationType[key] === type) || ''
}

/**
 * Convert notification type string to number representation
 * @param {string} typeString - The notification type string value
 * @returns {number} The numeric enum value of the notification type
 */
export const getNotificationTypeNumber = (typeString) => {
    return NotificationType[typeString] || 0
}

/**
 * Get display text for notification type
 * @param {number} type - The notification type enum value
 * @returns {string} The display text for the notification type
 */
export const getNotificationText = (type) => {
    switch (type) {
        case NotificationType.FRIEND_REQUEST:
            return 'sent you a friend request'
        case NotificationType.POST_LIKE:
            return 'liked your post'
        case NotificationType.POST_COMMENT:
            return 'commented on your post'
        case NotificationType.COMMENT_REPLY:
            return 'replied to your comment'
        default:
            return ''
    }
}
