<script setup>
import { useProfileStore } from '@/stores/profileStore'
import ProfileCover from './ProfileCover.vue'
import ProfileInfo from './ProfileInfo.vue'
import ProfileNavigation from './ProfileNavigation.vue'

const profileStore = useProfileStore()
</script>

<template>
    <header>
        <ProfileCover
            :coverUrl="profileStore.coverUrl"
            :isCurrentUser="profileStore.isCurrentUser"
            @change-cover="profileStore.handleChangeCover"
        />

        <ProfileInfo
            :userProfile="profileStore.profile"
            :isCurrentUser="profileStore.isCurrentUser"
            :isFriend="profileStore.isFriend"
            @edit-profile="profileStore.handleEditProfile"
            @change-avatar="profileStore.handleChangeAvatar"
            @friend-action="profileStore.handleFriendAction"
            @message="profileStore.handleMessage"
        />

        <ProfileNavigation
            :activeTab="profileStore.activeTab"
            @update:tab="profileStore.setActiveTab"
        />
    </header>
</template>
