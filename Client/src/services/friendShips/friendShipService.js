import { createResourcePath } from '@/utils/routeUtils'
import useAxios, { createAxiosConfig } from '../common/useAxios'

const useFriendshipService = () => {
    const controller = 'friend-ships'
    const defaultConfig = createAxiosConfig({ controller })
    const { baseService } = useAxios(defaultConfig)

    return {
        ...baseService,

        // Friend request methods
        sendRequest: (userId) => {
            const path = createResourcePath('requests', userId)
            return baseService.request('post', path)
        },

        acceptRequest: (requestId) => {
            const path = createResourcePath('requests', requestId, 'accept')
            return baseService.request('put', path)
        },

        declineRequest: (requestId) => {
            const path = createResourcePath('requests', requestId, 'decline')
            return baseService.request('put', path)
        },

        // Friend list methods
        getPendingRequests: () => {
            const path = createResourcePath('requests', 'pending')
            return baseService.request('get', path)
        },

        getFriends: (userId) => {
            const path = createResourcePath('users', userId, 'friends')
            return baseService.request('get', path)
        },

        // Other methods
        getFriendshipStatus: (userId) => {
            const path = createResourcePath('users', userId, 'status')
            return baseService.request('get', path)
        },

        removeFriend: (userId) => {
            const path = createResourcePath('users', userId)
            return baseService.request('delete', path)
        },
    }
}

export default useFriendshipService
