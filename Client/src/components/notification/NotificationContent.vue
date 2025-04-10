<template>
    <div class="relative group max-w-md w-full">
        <div class="flex items-center justify-between">
            <div class="flex items-center gap-3">
                <div
                    :class="[
                        'w-9 h-9 rounded-full flex items-center justify-center shadow-sm',
                        notification.isRead ? 'bg-surface-hover' : 'bg-notification-unread',
                    ]"
                >
                    <i
                        :class="getNotificationIcon(notification.type)"
                        class="text-primary-500 text-lg"
                    ></i>
                </div>
                <div class="flex-1 min-w-0 overflow-hidden">
                    <div
                        class="notification-title text-[15px] font-medium text-text-color mb-1 truncate"
                    >
                        {{ notification.title }}
                    </div>
                    <div
                        class="notification-message text-text-color-secondary text-[13px] leading-snug line-clamp-2 overflow-ellipsis"
                    >
                        {{ notification.message }}
                    </div>
                </div>
            </div>
            <Menu ref="menu" :model="menuItems" :popup="true">
                <template #trigger="{ toggle }">
                    <Button
                        icon="pi pi-ellipsis-h"
                        text
                        rounded
                        severity="secondary"
                        aria-label="Actions"
                        class="opacity-0 group-hover:opacity-100 transition-opacity p-2"
                        @click="toggle"
                    />
                </template>
            </Menu>
        </div>

        <!-- Dynamic notification actions -->
        <component
            :is="actionComponent"
            v-if="hasActions"
            :notification="notification"
            @action-complete="handleActionComplete"
        />

        <div class="flex items-center gap-4 mt-2.5">
            <span class="notification-time text-xs text-text-color-secondary opacity-75">
                {{ timeAgo(notification.createdAt) }}
            </span>
            <router-link
                v-if="notification.actionUrl"
                :to="notification.actionUrl"
                class="text-primary-500 hover:text-primary-600 text-xs flex items-center gap-1.5 font-medium"
            >
                View details
                <i class="pi pi-arrow-right text-[10px]"></i>
            </router-link>
        </div>
    </div>
</template>

<script setup>
import { ref, defineProps, computed } from 'vue'
import { NotificationType } from '@/common/enums/notificationType'
import { Menu, Button } from 'primevue'
import { useConfirm } from 'primevue/useconfirm'
import { timeAgo } from '@/utils/timeUtil'
import { useNotificationStore } from '@/stores/notificationStore'
import { getNotificationActions } from './actions'

const props = defineProps({
    notification: {
        type: Object,
        required: true,
    },
})

const menu = ref()
const confirm = useConfirm()
const notificationStore = useNotificationStore()

const actionComponent = computed(() => getNotificationActions(props.notification.type))
const hasActions = computed(() => actionComponent.value !== null)

const handleActionComplete = async ({ notification }) => {
    // Refresh notifications or update UI as needed after action completion
    // For now, we'll just mark the notification as read
    if (!notification.isRead) {
        await notificationStore.markAsRead(notification.id)
    }
}

const menuItems = [
    {
        label: props.notification.isRead ? 'Mark as unread' : 'Mark as read',
        icon: props.notification.isRead ? 'pi pi-envelope' : 'pi pi-check',
        command: () => handleMarkRead(),
    },
    {
        separator: true,
    },
    {
        label: 'Delete',
        icon: 'pi pi-trash',
        class: 'text-red-600 dark:text-red-400',
        command: () => handleDelete(),
    },
]

const handleDelete = () => {
    confirm.require({
        message: 'Are you sure you want to delete this notification?',
        header: 'Delete Notification',
        icon: 'pi pi-info-circle',
        acceptClass: 'p-button-danger',
        accept: async () => {
            try {
                await notificationStore.removeNotification(props.notification.id)
            } catch (error) {
                console.error('Error deleting notification:', error)
            }
        },
    })
}

const handleMarkRead = async () => {
    try {
        if (props.notification.isRead) {
            // API doesn't support mark as unread yet
            console.warn('Mark as unread not supported')
            return
        }
        await notificationStore.markAsRead(props.notification.id)
    } catch (error) {
        console.error('Error marking notification as read:', error)
    }
}

const getNotificationIcon = (type) => {
    switch (type) {
        case NotificationType.FRIEND_REQUEST:
            return 'pi pi-user-plus'
        case NotificationType.POST_LIKE:
            return 'pi pi-heart'
        case NotificationType.POST_COMMENT:
            return 'pi pi-comment'
        case NotificationType.COMMENT_REPLY:
            return 'pi pi-reply'
        default:
            return 'pi pi-bell'
    }
}
</script>

<style scoped>
.notification-message {
    word-break: break-word;
}
</style>
