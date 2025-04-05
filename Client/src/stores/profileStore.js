import useUserProfileService from '@/services/userProfiles/userProfileService'
import { useCustomToast } from '@/utils/toast/customToast'
import { defineStore } from 'pinia'
import { computed, ref } from 'vue'
import { useRoute } from 'vue-router'
import { useAuthStore } from './authStore'

export const useProfileStore = defineStore('profile', () => {
    const route = useRoute()
    const authStore = useAuthStore()
    const toast = useCustomToast()
    const userProfileService = useUserProfileService()

    // State
    const profile = ref({})
    const posts = ref([])
    const showEditProfile = ref(false)
    const showEditCover = ref(false)
    const showEditAvatar = ref(false)
    const activeTab = ref('posts')

    // Computed
    const isCurrentUser = computed(() => {
        return authStore.user.userId === route.params.id
    })

    const isFriend = computed(() => {
        return profile.value?.isFriend || false
    })

    // Tab Actions
    const setActiveTab = (tab) => {
        activeTab.value = tab
    }

    // Profile Actions
    const loadProfile = async () => {
        const response = await userProfileService.getById(route.params.id).catch(() => {
            toast.showError('Failed to load profile')
            return { content: null }
        })
        if (response.content) {
            profile.value = response.content
        }
    }

    const updateProfile = async (profileData) => {
        const response = await userProfileService.updateProfile(profileData)
        profile.value = { ...profile.value, ...response.content }

        // Update auth store if it's current user
        if (isCurrentUser.value) {
            authStore.user = {
                ...authStore.user,
                firstName: profileData.firstName,
                lastName: profileData.lastName,
            }
        }

        toast.showSuccess('Profile updated successfully')
        showEditProfile.value = false
    }

    // Avatar Actions
    const updateAvatar = async (file) => {
        const response = await userProfileService.updateAvatar(file)
        profile.value.avatarUrl = response.content.avatarUrl

        // Update auth store if it's current user
        if (isCurrentUser.value) {
            authStore.user.avatarUrl = response.content.avatarUrl
        }

        toast.showSuccess('Avatar updated successfully')
        showEditAvatar.value = false
    }

    const removeAvatar = async () => {
        await userProfileService.removeAvatar()
        profile.value.avatarUrl = null

        // Update auth store if it's current user
        if (isCurrentUser.value) {
            authStore.user.avatarUrl = null
        }

        toast.showSuccess('Avatar removed successfully')
        showEditAvatar.value = false
    }

    // Cover Actions
    const updateCover = async ({ file, position }) => {
        const response = await userProfileService.updateCover({ file, position })
        profile.value.coverUrl = response.content.coverUrl
        if (response.content.coverPosition) {
            profile.value.coverPosition = response.content.coverPosition
        }
        toast.showSuccess('Cover photo updated successfully')
        showEditCover.value = false
    }

    const removeCover = async () => {
        await userProfileService.removeCover()
        profile.value.coverUrl = null
        profile.value.coverPosition = null

        toast.showSuccess('Cover photo removed successfully')
        showEditCover.value = false
    }

    // Friend Actions
    const handleFriendAction = async () => {
        if (isFriend.value) {
            await userProfileService.unfriend(route.params.id)
        } else {
            await userProfileService.addFriend(route.params.id)
        }
        await loadProfile()
    }

    // Message Action
    const handleMessage = () => {
        // TODO: Implement messaging functionality
        console.log('Message action not implemented yet')
    }

    // Post Actions
    const handleCreatePost = async (postData) => {
        await userProfileService.createPost(postData)
        toast.showSuccess('Post created successfully')
        await loadProfile()
    }

    return {
        // State
        profile,
        posts,
        loading: userProfileService.isLoading,
        showEditProfile,
        showEditCover,
        showEditAvatar,
        activeTab,

        // Computed
        isCurrentUser,
        isFriend,

        // Tab Actions
        setActiveTab,

        // Profile Actions
        loadProfile,
        updateProfile,

        // Avatar Actions
        updateAvatar,
        removeAvatar,

        // Cover Actions
        updateCover,
        removeCover,

        // Friend Actions
        handleFriendAction,

        // Message Actions
        handleMessage,

        // Post Actions
        handleCreatePost,
    }
})
