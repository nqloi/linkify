import { createResourcePath } from '@/utils/routeUtils'
import useAxios, { createAxiosConfig } from '../common/useAxios'
import { getCursorPaginationParams } from '@/utils/paginationUtil'

const usePostService = () => {
    const controller = 'posts'

    const defaultConfig = createAxiosConfig({
        controller,
        headers: {
            'Content-Type': 'multipart/form-data',
        },
    })

    const { baseService } = useAxios(defaultConfig)

    const getCursorPagedUserPosts = (userId, pagingParams = {}) => {
        const path = createResourcePath('users', userId)
        return baseService.request('get', path, null, {
            ...getCursorPaginationParams(),
            ...pagingParams,
        })
    }

    return {
        ...baseService,
        getCursorPagedUserPosts,
    }
}

export default usePostService
