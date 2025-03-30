import { CACHE_KEYS } from '@/utils/cache/cacheConstants'
import useCache from '@/utils/cache/useCache'

export const createAuthInterceptor = () => {
    const { getCache } = useCache()

    return {
        request: (config) => {
            const token = getCache(CACHE_KEYS.ACCESS_TOKEN)
            if (token) {
                config.headers.Authorization = `Bearer ${token}`
            }
            return config
        },

        response: (error) => {
            return Promise.reject(error)
        },
    }
}
