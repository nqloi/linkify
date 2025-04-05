<script setup>
import { useProfileStore } from '@/stores/profileStore'
import { useCustomToast } from '@/utils/toast/customToast'
import ProfileCover from './ProfileCover.vue'
import ProfileInfo from './ProfileInfo.vue'
import ProfileNavigation from './ProfileNavigation.vue'
import EditProfileModal from '@/views/profile/modals/EditProfileModal.vue'
import EditAvatarModal from '@/views/profile/modals/EditAvatarModal.vue'
import EditCoverModal from '@/views/profile/modals/EditCoverModal.vue'

const profileStore = useProfileStore()
const toast = useCustomToast()

const handleProfileUpdate = async (profileData) => {
    try {
        await profileStore.updateProfile(profileData)
        profileStore.showEditProfile = false
    } catch (error) {
        toast.showError('Failed to update profile')
    }
}

const handleAvatarUpdate = async (file) => {
    try {
        const formData = new FormData()
        formData.append('avatar', file)
        await profileStore.updateAvatar(formData)
        profileStore.showEditAvatar = false
    } catch (error) {
        toast.showError('Failed to update avatar')
    }
}

const handleCoverUpdate = async ({ file, position }) => {
    try {
        const formData = new FormData()
        formData.append('cover', file)
        formData.append('position', position)
        await profileStore.updateCover(formData)
        profileStore.showEditCover = false
    } catch (error) {
        toast.showError('Failed to update cover photo')
    }
}

const handleAvatarRemove = async () => {
    try {
        await profileStore.removeAvatar()
        profileStore.showEditAvatar = false
    } catch (error) {
        toast.showError('Failed to remove avatar')
    }
}

const handleCoverRemove = async () => {
    try {
        await profileStore.removeCover()
        profileStore.showEditCover = false
    } catch (error) {
        toast.showError('Failed to remove cover photo')
    }
}
</script>

<template>
    <header>
        <ProfileCover
            :coverUrl="profileStore.profile.coverUrl"
            :coverPosition="profileStore.profile.coverPosition"
            :isCurrentUser="profileStore.isCurrentUser"
            @change-cover="profileStore.showEditCover = true"
        />

        <ProfileInfo
            :userProfile="profileStore.profile"
            :isCurrentUser="profileStore.isCurrentUser"
            :isFriend="profileStore.isFriend"
            @edit-profile="profileStore.showEditProfile = true"
            @change-avatar="profileStore.showEditAvatar = true"
            @friend-action="profileStore.handleFriendAction"
            @message="profileStore.handleMessage"
        />

        <ProfileNavigation
            :activeTab="profileStore.activeTab"
            @update:tab="profileStore.setActiveTab"
        />

        <!-- Modals -->
        <EditProfileModal
            v-model:visible="profileStore.showEditProfile"
            :userProfile="profileStore.profile"
            @save="handleProfileUpdate"
        />

        <EditAvatarModal
            v-model:visible="profileStore.showEditAvatar"
            :currentAvatarUrl="profileStore.profile.avatarUrl"
            @save="handleAvatarUpdate"
            @remove="handleAvatarRemove"
        />

        <EditCoverModal
            v-model:visible="profileStore.showEditCover"
            :currentCoverUrl="profileStore.profile.coverUrl"
            :currentPosition="profileStore.profile.coverPosition"
            @save="handleCoverUpdate"
            @remove="handleCoverRemove"
        />
    </header>
</template>
