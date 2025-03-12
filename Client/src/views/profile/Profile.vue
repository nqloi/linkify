<template>
    <div class="profile-component flex flex-col items-center w-full">
        <!-- Avatar Section -->
        <div class="avatar-section mb-6">
            <UserAvatar :avatarUrl="user.avatarUrl" :size="SIZE.XXL" />
        </div>

        <!-- User Info Section -->
        <div class="user-info-section text-center">
            <div class="full-name text-gray-600 mb-4">{{ userProfile.displayName }}</div>
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
import { onMounted, ref } from 'vue'
import { useToast } from 'primevue'
import { onChatMessageReceived } from '@/services/chat/useChatService'
import useUserProfileService from '@/services/userProfiles/userProfileService'
import { useRoute } from 'vue-router'

const toast = useToast()
const authStore = useAuthStore()
const route = useRoute()
const { user, fetchCurrentUser } = authStore
const userProfile = ref({})
const userProfileService = useUserProfileService()

onMounted(async () => {
    const res = await userProfileService.getById(route.params.id)
    userProfile.value = res.content
    // onChatMessageReceived((msg) => console.log(msg))
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
