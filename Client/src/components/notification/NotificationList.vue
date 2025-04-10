<template>
    <div class="h-full notification-list">
        <template v-if="notificationStore.loading && !notificationStore.notifications.length">
            <div class="space-y-1 p-1">
                <div v-for="n in 3" :key="n" class="p-4">
                    <div class="flex gap-3 items-center">
                        <Skeleton shape="circle" size="3rem" />
                        <div class="flex-1">
                            <Skeleton class="mb-3" height="1.25rem" width="85%" />
                            <Skeleton height="0.875rem" width="65%" />
                            <div class="mt-2 flex gap-2">
                                <Skeleton width="4rem" height="0.75rem" />
                                <Skeleton width="5rem" height="0.75rem" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </template>

        <template v-else-if="notificationStore.notifications.length">
            <div class="h-full">
                <InfiniteScroll
                    :loading="notificationStore.loading"
                    :error="notificationStore.error"
                    :has-more="notificationStore.hasNextPage"
                    @load-more="notificationStore.loadMore"
                    @retry="notificationStore.loadMore"
                    end-message="No more notifications"
                    threshold="200"
                >
                    <div
                        v-for="notification in notificationStore.notifications"
                        :key="notification.id"
                        class="notification-item py-3 px-4 cursor-pointer transition-all duration-200 rounded-md"
                        :class="[
                            notification.isRead
                                ? 'hover:bg-notification-hover'
                                : 'bg-notification-unread hover:bg-notification-unread-hover',
                            'border-b border-notification-divider last:border-b-0',
                        ]"
                        @click="handleClick(notification)"
                    >
                        <NotificationContent :notification="notification" />
                    </div>
                </InfiniteScroll>
            </div>
        </template>

        <!-- Empty State -->
        <div
            v-else
            class="py-12 px-4 text-center text-text-color-secondary flex flex-col items-center"
        >
            <i class="pi pi-bell text-5xl mb-4 opacity-40"></i>
            <p class="text-lg font-medium mb-1">No notifications yet</p>
            <p class="text-sm opacity-75">We'll notify you when something happens</p>
        </div>
    </div>
</template>

<script setup>
import { useNotificationStore } from '@/stores/notificationStore'
import { useRouter } from 'vue-router'
import NotificationContent from './NotificationContent.vue'
import InfiniteScroll from '@/components/common/InfiniteScroll.vue'
import Skeleton from 'primevue/skeleton'
import { logger } from '@/utils/logger'

const notificationStore = useNotificationStore()
const router = useRouter()

const handleClick = async (notification) => {
    try {
        // Mark as read first
        await notificationStore.markAsRead(notification.id)

        // Handle based on actionUrl
        if (notification.actionUrl) {
            router.push(notification.actionUrl)
        }
    } catch (error) {
        logger.error('Failed to handle notification click', { error })
    }
}
</script>
