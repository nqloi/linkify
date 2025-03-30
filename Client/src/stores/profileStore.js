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
    const loading = ref(false)
    const showEditProfile = ref(false)
    const showEditCover = ref(false)
    const showEditAvatar = ref(false)

    // Computed
    const isCurrentUser = computed(() => {
        return authStore.user.userId === route.params.id
    })

    const isFriend = computed(() => {
        return profile.value?.isFriend || false
    })

    // Actions
    const loadProfile = async () => {
        try {
            loading.value = true
            const response = await userProfileService.getById(route.params.id)
            profile.value = response.content
        } catch (error) {
            toast.showCustomError('Failed to load profile')
        } finally {
            loading.value = false
        }
    }

    const handleEditProfile = () => {
        showEditProfile.value = true
    }

    const handleChangeCover = () => {
        showEditCover.value = true
    }

    const handleChangeAvatar = () => {
        showEditAvatar.value = true
    }

    const handleFriendAction = async () => {
        try {
            loading.value = true
            if (isFriend.value) {
                await userProfileService.unfriend(route.params.id)
            } else {
                await userProfileService.addFriend(route.params.id)
            }
            await loadProfile()
        } catch (error) {
            toast.add({
                severity: 'error',
                summary: 'Error',
                detail: 'Failed to update friend status',
            })
        } finally {
            loading.value = false
        }
    }

    const handleCreatePost = async (postData) => {
        try {
            loading.value = true
            await userProfileService.createPost(postData)
            toast.add({
                severity: 'success',
                summary: 'Success',
                detail: 'Post created successfully',
            })
        } catch (error) {
            toast.add({
                severity: 'error',
                summary: 'Error',
                detail: 'Failed to create post',
            })
        } finally {
            loading.value = false
        }
    }

    return {
        // State
        profile,
        posts,
        loading,
        showEditProfile,
        showEditCover,
        showEditAvatar,

        // Computed
        isCurrentUser,
        isFriend,

        // Actions
        loadProfile,
        handleEditProfile,
        handleChangeCover,
        handleChangeAvatar,
        handleFriendAction,
        handleCreatePost,
    }
})
