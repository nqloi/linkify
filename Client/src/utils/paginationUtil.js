import { DEFAULT_PAGINATION } from '@/common/constants/pagination'

export const getCursorPaginationParams = (options = {}) => {
    const { limit = DEFAULT_PAGINATION.limit, cursor = DEFAULT_PAGINATION.cursor } = options
    return { limit, cursor }
}
