<template>
    <div class="profile-component flex flex-col items-center w-full">
        <!-- Avatar Section -->
        <div class="avatar-section mb-6">
            <UserAvatar :avatarUrl="user.avatarUrl" :size="SIZE.XXL" />
        </div>

        <!-- User Info Section -->
        <div class="user-info-section text-center">
            <div class="username font-bold text-2xl mb-1">
                {{ user.username }}
            </div>
            <div class="full-name text-gray-600 mb-4">{{ user.firstName }} {{ user.lastName }}</div>
        </div>

        <!-- Actions Section -->
        <div class="actions-section flex gap-4">
            <Button
                label="Edit Profile"
                outlined
                icon="pi pi-pencil"
                @click="handleEditProfile"
                class="rounded-full"
            />
            <Button
                label="Add Friends"
                icon="pi pi-user-plus"
                @click="handleAddFriends"
                class="rounded-full"
            />
        </div>
    </div>
</template>

<script setup>
import { useAuthStore } from '@/stores/authStore'
import UserAvatar from '@/components/common/UserAvatar.vue'
import { SIZE } from '@/common/constants/size'
import { onMounted } from 'vue'
import { useToast } from 'primevue'
import { onChatMessageReceived } from '@/services/chat/useChatService'
const toast = useToast()
const authStore = useAuthStore()
const { user, fetchCurrentUser } = authStore

onMounted(async () => {
    if (!user) {
        await fetchCurrentUser()
    }

    onChatMessageReceived((msg) => console.log(msg))
})

const handleEditProfile = () => {
    toast.add({
        severity: 'info',
        summary: 'Info',
        detail: 'Edit profile clicked',
        life: 3000,
    })
}

const handleAddFriends = () => {
    toast.add({
        severity: 'info',
        summary: 'Info',
        detail: 'Add friends clicked',
        life: 3000,
    })
}
</script>

<style lang="scss" scoped>
.profile-component {
    width: 100%;
    max-width: 500px;
}

.user-info-section {
    .username {
        white-space: nowrap;
    }
}
</style>
