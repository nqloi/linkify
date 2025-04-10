<template>
    <div class="profile-info relative -mt-12 pb-4 z-20">
        <!-- Added z-index -->
        <div class="max-w-7xl mx-auto px-4">
            <div class="flex flex-col md:flex-row items-end md:items-end gap-4">
                <!-- Added padding-top -->
                <!-- Avatar Section -->
                <div class="relative">
                    <div
                        class="w-40 h-40 rounded-full border-4 border-white overflow-hidden bg-white"
                    >
                        <img
                            :src="userProfile.avatarUrl || defaultAvatar"
                            class="w-full h-full object-cover"
                            alt="Profile photo"
                            @error="handleImageError"
                        />
                    </div>
                    <Button
                        v-if="isCurrentUser"
                        icon="pi pi-camera"
                        class="absolute bottom-2 right-2 bg-white"
                        rounded
                        variant="outlined"
                        severity="secondary"
                        @click="$emit('change-avatar')"
                    />
                </div>

                <!-- User Info Section -->
                <div
                    class="flex-1 flex flex-col md:flex-row justify-between items-start md:items-center gap-4"
                >
                    <div class="user-details">
                        <h1 class="text-3xl font-bold">{{ userProfile.displayName }}</h1>
                        <div class="flex items-center gap-2 text-gray-600">
                            <span>{{ userProfile.friendsCount }} friends</span>
                            <span v-if="userProfile.mutualFriends" class="text-sm">
                                · {{ userProfile.mutualFriends }} mutual friends
                            </span>
                        </div>
                        <div v-if="userProfile.education" class="text-sm text-gray-600 mt-1">
                            <i class="pi pi-book mr-2"></i>
                            {{ userProfile.education }}
                        </div>
                    </div>

                    <!-- Action Buttons -->
                    <div class="flex gap-3">
                        <Button
                            v-if="isCurrentUser"
                            label="Edit Profile"
                            icon="pi pi-pencil"
                            @click="$emit('edit-profile')"
                            class="rounded-full"
                        />
                        <Button
                            v-else
                            :label="isFriend ? 'Friends' : 'Add Friend'"
                            :icon="isFriend ? 'pi pi-check' : 'pi pi-user-plus'"
                            :class="{ 'p-button-outlined': isFriend }"
                            @click="handleFriendRequest"
                            class="rounded-full"
                        />
                        <Button
                            v-if="!isCurrentUser"
                            label="Message"
                            icon="pi pi-send"
                            outlined
                            @click="$emit('message')"
                            class="rounded-full"
                        />
                        <Button
                            v-if="!isCurrentUser"
                            icon="pi pi-ellipsis-h"
                            @click="$emit('more-actions')"
                            class="rounded-full"
                        />
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import defaultAvatar from '@/assets/images/avatar-default.svg'
import useFriendshipService from '@/services/friendShips/friendShipService'
import Button from 'primevue/button'
import { useToast } from 'primevue/usetoast'

const props = defineProps({
    userProfile: {
        type: Object,
        required: true,
        default: () => ({}),
    },
    isCurrentUser: {
        type: Boolean,
        default: false,
    },
    isFriend: {
        type: Boolean,
        default: false,
    },
})

const emit = defineEmits([
    'edit-profile',
    'change-avatar',
    'message',
    'more-actions',
    'friend-action',
])

const toast = useToast()
const friendshipService = useFriendshipService()

const handleFriendRequest = async () => {
    if (props.isFriend) {
        return handleRemoveFriend()
    }

    try {
        const response = await friendshipService.sendRequest(props.userProfile.userId)
        if (!response) return

        // Update parent component with new friend request status
        emit('friend-action', { type: 'request', userId: props.userProfile.userId })

        toast.add({
            severity: 'success',
            summary: 'Friend Request Sent',
            detail: `Friend request sent to ${props.userProfile.displayName}`,
            life: 3000,
        })
    } catch (error) {
        console.error('Error sending friend request:', error)
        toast.add({
            severity: 'error',
            summary: 'Error',
            detail: error.response?.data?.message || 'Failed to send friend request',
            life: 3000,
        })
    }
}

const handleRemoveFriend = async () => {
    try {
        const response = await friendshipService.removeFriend(props.userProfile.userId)
        if (!response) return

        // Update parent component with removed friend status
        emit('friend-action', { type: 'remove', userId: props.userProfile.userId })

        toast.add({
            severity: 'info',
            summary: 'Friend Removed',
            detail: `${props.userProfile.displayName} has been removed from your friends`,
            life: 3000,
        })
    } catch (error) {
        console.error('Error removing friend:', error)
        toast.add({
            severity: 'error',
            summary: 'Error',
            detail: error.response?.data?.message || 'Failed to remove friend',
            life: 3000,
        })
    }
}

const handleImageError = (e) => {
    e.target.src = '/default-avatar.jpg'
}
</script>

<style scoped>
.profile-info {
    width: 100%;
    pointer-events: none; /* Add this */
}

/* Add this to allow interactions with specific elements */
.profile-info * {
    pointer-events: auto;
}

@media (max-width: 768px) {
    .user-details {
        text-align: center;
        width: 100%;
    }
}
</style>
