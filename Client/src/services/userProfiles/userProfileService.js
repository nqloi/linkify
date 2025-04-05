import useAxios from '../common/useAxios'

const useUserProfileService = () => {
    const controller = 'user-profiles'
    const { baseService, isLoading } = useAxios(controller)

    return {
        ...baseService,
        isLoading,

        // Profile methods
        updateProfile: (data) => baseService.update('profile', data),

        // Avatar methods
        updateAvatar: (file) => baseService.upload('avatar', { file }),
        removeAvatar: () => baseService.delete('avatar'),

        // Cover methods
        updateCover: ({ file, position }) => baseService.upload('cover', { file, position }),
        removeCover: () => baseService.delete('cover'),

        // Friend methods
        addFriend: (userId) => baseService.create(`friends/${userId}`),
        unfriend: (userId) => baseService.delete(`friends/${userId}`),

        // Post methods
        createPost: (data) => {
            if (data.mediaFiles?.length > 0) {
                return baseService.upload('posts', data)
            }
            return baseService.create('posts', data)
        },

        // Get user profile data
        getUserPosts: (userId, params) => baseService.getAll(`${userId}/posts`, params),
        getUserFriends: (userId, params) => baseService.getAll(`${userId}/friends`, params),
        getUserPhotos: (userId, params) => baseService.getAll(`${userId}/photos`, params),
    }
}

export default useUserProfileService
