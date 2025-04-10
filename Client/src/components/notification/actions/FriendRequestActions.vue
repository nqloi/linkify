<template>
    <base-notification-actions :notification="notification">
        <Button
            label="Decline"
            severity="secondary"
            size="small"
            :loading="loading"
            @click="handleDecline"
        />
        <Button
            label="Accept"
            severity="primary"
            size="small"
            :loading="loading"
            @click="handleAccept"
        />
    </base-notification-actions>
</template>

<script setup>
import { ref } from 'vue'
import { useToast } from 'primevue/usetoast'
import Button from 'primevue/button'
import BaseNotificationActions from './BaseNotificationActions.vue'
import useFriendshipService from '@/services/friendShips/friendShipService'
import { logger } from '@/utils/logger'

const props = defineProps({
    notification: {
        type: Object,
        required: true,
    },
})

const loading = ref(false)
const toast = useToast()
const friendshipService = useFriendshipService()

const handleAccept = async (e) => {
    e.stopPropagation()

    try {
        // loading.value = true
        // const response = await friendshipService.acceptRequest(props.notification.id)
        // if (!response) return

        toast.add({
            severity: 'success',
            summary: 'Friend Request Accepted',
            detail: `You are now friends with ${props.notification.sender?.displayName}`,
            life: 3000,
        })
    } catch (error) {
        logger.error('Error accepting friend request:', error)
    } finally {
        loading.value = false
    }
}

const handleDecline = async () => {
    try {
        loading.value = true
        const response = await friendshipService.declineRequest(props.notification.id)
        if (!response) return

        toast.add({
            severity: 'info',
            summary: 'Friend Request Declined',
            life: 3000,
        })
    } catch (error) {
        logger.error('Error declining friend request:', error)
    } finally {
        loading.value = false
    }
}
</script>
