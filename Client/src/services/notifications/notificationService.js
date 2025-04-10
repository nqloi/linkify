import { getCursorPaginationParams } from '@/utils/paginationUtil'
import useAxios from '../common/useAxios'
import { createResourcePath } from '@/utils/routeUtils'

const useNotificationService = () => {
    const controller = 'notifications'
    const { baseService } = useAxios(controller)

    return {
        ...baseService,

        getAll: (params = null) => {
            return baseService.request('get', '', null, params)
        },

        getPaginatedNotifications: (pagingParams) => {
            const path = createResourcePath('paginated')
            return baseService.request('get', path, null, {
                ...getCursorPaginationParams(),
                ...pagingParams,
            })
        },

        markAsRead: (notificationId) => {
            const path = createResourcePath(notificationId, 'read')
            return baseService.request('put', path)
        },

        markAllAsRead: () => {
            const path = createResourcePath('mark-all-read')
            return baseService.request('put', path)
        },

        delete: (notificationId) => {
            return baseService.request('delete', notificationId)
        },

        clearAll: () => {
            const path = createResourcePath('clear-all')
            return baseService.request('delete', path)
        },

        getUnreadCount: () => {
            const path = createResourcePath('unread-count')
            return baseService.request('get', path)
        },
    }
}

export default useNotificationService
