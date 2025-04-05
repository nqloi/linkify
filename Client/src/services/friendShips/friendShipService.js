import { createResourcePath } from '@/utils/routeUtils'
import useAxios, { createAxiosConfig } from '../common/useAxios'

const useFriendShipService = () => {
    const controller = 'friend-ships'

    const defaultConfig = createAxiosConfig({
        controller,
    })
    const { baseService } = useAxios(defaultConfig)

    const sendRequestAddFriend = (userId) => {
        const url = createResourcePath('requests', userId)
        return baseService.request('POST', url)
    }

    return {
        ...baseService,
        sendRequestAddFriend,
    }
}

export default useFriendShipService
