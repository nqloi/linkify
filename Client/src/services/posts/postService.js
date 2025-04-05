import { createResourcePath } from '@/utils/routeUtils'
import useAxios, { createAxiosConfig } from '../common/useAxios'
import { getCursorPaginationParams } from '@/utils/paginationUtil'

const usePostService = () => {
    const controller = 'posts'

    const defaultConfig = createAxiosConfig({
        controller,
    })

    const { baseService } = useAxios(defaultConfig)

    const create = (data) => {
        const { baseService } = useAxios({
            controller,
            headers: {
                'Content-Type': 'multipart/form-data',
            },
        })

        return baseService.create(data)
    }

    const getCursorPagedUserPosts = (userId, pagingParams = {}) => {
        const path = createResourcePath('users', userId)
        return baseService.request('get', path, null, {
            ...getCursorPaginationParams(),
            ...pagingParams,
        })
    }

    return {
        ...baseService,
        create,
        getCursorPagedUserPosts,
    }
}

export default usePostService
