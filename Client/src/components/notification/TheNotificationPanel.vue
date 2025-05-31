<template>
    <div class="relative">
        <OverlayBadge
            class="notification-icon cursor-pointer bg-slate-100 hover:bg-gray-200 rounded-full dark:hover:bg-surface-hover transition-colors duration-200"
            :value="notificationStore.unreadCount"
            @click="togglePanel"
        >
            <i class="pi pi-bell p-3 text-xl" />
        </OverlayBadge>
        <Popover
            ref="notificationPanel"
            :closeOnEscape="true"
            @show="onShow"
            class="notification-panel w-[400px]"
            :breakpoints="{
                '960px': '100vw',
            }"
            :showCloseIcon="false"
        >
            <template #trigger>
                <span class="hidden"></span>
            </template>
            <div class="notification-content">
                <div
                    class="p-4 border-b flex justify-between items-center bg-surface-card border-notification-divider"
                >
                    <div class="flex items-center gap-3">
                        <h3 class="font-semibold text-lg">Notifications</h3>
                        <span
                            v-if="notificationStore.unreadCount"
                            class="text-sm text-text-color-secondary"
                        >
                            ({{ notificationStore.unreadCount }} unread)
                        </span>
                        <Button
                            icon="pi pi-refresh"
                            link
                            :loading="refreshing"
                            size="small"
                            @click="handleRefresh"
                        />
                    </div>
                    <div
                        class="flex items-center gap-2"
                        v-if="notificationStore.notifications.length"
                    >
                        <Button
                            v-if="notificationStore.unreadCount"
                            link
                            size="small"
                            icon="pi pi-check-circle"
                            class="w-full"
                            :loading="markingAllRead"
                            @click="handleMarkAllRead"
                        >
                            Mark all read
                        </Button>
                        <Button
                            link
                            size="small"
                            icon="pi pi-trash"
                            class="text-red-500"
                            :loading="clearingAll"
                            @click="handleClearAll"
                            v-tooltip.bottom="'Clear all notifications'"
                        />
                    </div>
                </div>
                <div class="notification-list-wrapper">
                    <NotificationList />
                </div>
            </div>
        </Popover>
    </div>
</template>

<script setup>
import { useNotificationStore } from '@/stores/notificationStore'
import { logger } from '@/utils/logger'
import { Button, OverlayBadge } from 'primevue'
import Popover from 'primevue/popover'
import { useConfirm } from 'primevue/useconfirm'
import { ref } from 'vue'
import NotificationList from './NotificationList.vue'

const confirm = useConfirm()
const notificationStore = useNotificationStore()

const notificationPanel = ref()
const markingAllRead = ref(false)
const clearingAll = ref(false)
const refreshing = ref(false)

const handleRefresh = async () => {
    refreshing.value = true
    try {
        notificationStore.resetState()
        await notificationStore.keepLoadedData()
    } catch (error) {
        logger.error('Failed to refresh notifications', { error })
    } finally {
        refreshing.value = false
    }
}

const togglePanel = (event) => {
    notificationPanel.value?.toggle(event)
}

const onShow = async () => {
    try {
        await notificationStore.keepLoadedData()
    } catch (error) {
        logger.error('Failed to load notifications', { error })
    }
}

const handleClearAll = () => {
    confirm.require({
        message: 'Are you sure you want to clear all notifications? This cannot be undone.',
        header: 'Clear All Notifications',
        icon: 'pi pi-info-circle',
        acceptClass: 'p-button-danger',
        accept: async () => {
            clearingAll.value = true
            try {
                await notificationStore.clearAll()
            } catch (error) {
                logger.error('Failed to clear notifications', { error })
            } finally {
                clearingAll.value = false
            }
        },
    })
}

const handleMarkAllRead = async () => {
    markingAllRead.value = true
    try {
        await notificationStore.markAllAsRead()
    } catch (error) {
        logger.error('Failed to mark notifications as read', { error })
    } finally {
        markingAllRead.value = false
    }
}
</script>

<style lang="scss" scoped>
.notification-panel {
    width: 700px;

    .notification-content {
        display: flex;
        flex-direction: column;
        height: 100%;
        overflow: hidden;
    }

    .notification-list-wrapper {
        overflow-y: auto;
        max-height: 60vh;
    }

    :deep(.p-button) {
        transition: background-color var(--transition-duration);
    }

    :deep(.p-button.p-button-text:not(:disabled):hover) {
        background: var(--surface-hover);
    }

    :deep(.p-skeleton) {
        background: var(--skeleton-bg);

        &::after {
            background: var(--skeleton-animation-bg);
        }
    }
}

.notification-icon {
    :deep(.p-badge) {
        transform: translate(50%, -40%) !important;
    }
}
</style>
