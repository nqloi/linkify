import useAxios, { createAxiosConfig } from '../common/useAxios'
import { createResourcePath } from '@/utils/routeUtils'

const useCommentService = (postId) => {
    const controller = createResourcePath('posts', postId, 'comments')

    const defaultConfig = createAxiosConfig({
        controller,
    })

    const { baseService } = useAxios(defaultConfig)

    return {
        ...baseService,
    }
}

export default useCommentService
