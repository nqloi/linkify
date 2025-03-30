import { useAuthStore } from '@/stores/authStore'
import useCache from '@/utils/cache/useCache'
import { CACHE_KEYS } from '@/utils/cache/cacheConstants'

let isRefreshing = false
let failedQueue = []

const processQueue = (error, token = null) => {
    failedQueue.forEach(({ resolve, reject }) => {
        error ? reject(error) : resolve(token)
    })
    failedQueue = []
}

export const createRefreshTokenInterceptor = (instance, refreshTokenUrl) => {
    const refreshToken = async () => {
        const { getCache, setCache } = useCache()
        const { onSessionTimeout } = useAuthStore()

        try {
            const refreshToken = getCache(CACHE_KEYS.REFRESH_TOKEN)
            if (!refreshToken) throw new Error('No refresh token available')

            const response = await instance.post(refreshTokenUrl, {
                refreshToken,
            })
            const { accessToken, refreshToken: newRefreshToken } = response.content

            if (!accessToken) throw new Error('Failed to refresh access token')

            setCache(CACHE_KEYS.ACCESS_TOKEN, accessToken)
            setCache(CACHE_KEYS.REFRESH_TOKEN, newRefreshToken)

            return accessToken
        } catch (error) {
            onSessionTimeout()
            throw error
        }
    }

    return {
        response: async (error) => {
            const { response: errorResponse, config: originalRequest } = error

            if (![401, 403].includes(errorResponse?.status) || originalRequest._retry) {
                return Promise.reject(error)
            }

            if (isRefreshing) {
                return new Promise((resolve, reject) => {
                    failedQueue.push({ resolve, reject })
                })
                    .then((token) => {
                        originalRequest.headers.Authorization = `Bearer ${token}`
                        return instance(originalRequest)
                    })
                    .catch((err) => Promise.reject(err))
            }

            originalRequest._retry = true
            isRefreshing = true

            try {
                const newToken = await refreshToken()
                originalRequest.headers.Authorization = `Bearer ${newToken}`
                processQueue(null, newToken)
                return instance(originalRequest)
            } catch (refreshError) {
                processQueue(refreshError)
                return Promise.reject(refreshError)
            } finally {
                isRefreshing = false
            }
        },
    }
}
