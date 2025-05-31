<template>
    <Toast position="bottom-right" :group="TOAST_GROUP.NOTIFICATION" @close="handleClose">
        <template #message="slotProps">
            <div class="w-full">
                <div class="flex items-center gap-3">
                    <UserAvatar :user="slotProps.message.sender" size="sm" />
                    <div class="flex-1 min-w-0">
                        <div class="font-medium text-text-color line-clamp-1">New notification</div>
                        <div class="text-sm text-text-color-secondary line-clamp-2">
                            {{ slotProps.message.summary }}
                        </div>
                    </div>
                    <i
                        :class="getNotificationIcon(slotProps.message.detail?.type)"
                        class="text-primary-500 mr-2"
                    ></i>
                </div>

                <div class="mt-2 flex justify-end">
                    <Button
                        size="small"
                        text
                        class="p-1 text-primary-500"
                        @click="() => handleAction(slotProps.message)"
                    >
                        <i class="pi pi-eye mr-1"></i>
                        View
                    </Button>
                </div>
            </div>
        </template>
    </Toast>
</template>

<script setup>
import TOAST_GROUP from '@/common/constants/toastGroup'
import { NotificationType } from '@/common/enums/notificationType'
import UserAvatar from '@/components/common/UserAvatar.vue'
import { Toast } from 'primevue'
import { useRouter } from 'vue-router'

const router = useRouter()

const handleAction = (message) => {
    if (message.detail?.actionUrl) {
        router.push(message.detail.actionUrl)
    }
}

const handleClose = () => {
    console.log('close')
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

<style scoped></style>
