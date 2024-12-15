import axios from 'axios'
import { ref } from 'vue'
import useCache from '@/utils/cache/useCache'
import { CACHE_KEYS } from '@/utils/cache/cacheContants'
import { useAuthStore } from '@/stores/authStore'

const defaultVersion = 'v1'
let isRefreshing = false
let failedQueue = []

const processQueue = (error, token = null) => {
    failedQueue.forEach(({ resolve, reject }) => {
        error ? reject(error) : resolve(token)
    })
    failedQueue = []
}

const useAxios = (controller, version = defaultVersion, customHeader = {}) => {
    const isLoading = ref(false)

    const instance = axios.create({
        baseURL: `https://localhost:7029/api/${version}/${controller}`,
        headers: {
            accept: 'application/json',
            'Content-Type': 'application/json',
            ...customHeader,
        },
    })

    const refreshTokenHandler = async () => {
        const { getCache, setCache } = useCache()
        const { onSessionTimeout } = useAuthStore()
        try {
            const refreshToken = getCache(CACHE_KEYS.REFRESH_TOKEN)
            if (!refreshToken) throw new Error('No refresh token available')

            const response = await instance.post(
                'https://localhost:7029/api/v1/auth/refresh-token',
                refreshToken,
            )
            const { accessToken } = response.content

            if (!accessToken) throw new Error('Failed to refresh access token')

            setCache(CACHE_KEYS.ACCESS_TOKEN, accessToken)
            processQueue(null, accessToken)
            return accessToken
        } catch (error) {
            processQueue(error)
            onSessionTimeout()
            throw error
        } finally {
            isRefreshing = false
        }
    }

    instance.interceptors.request.use(
        (config) => {
            const { getCache } = useCache()
            isLoading.value = true
            const token = getCache(CACHE_KEYS.ACCESS_TOKEN)
            if (token) {
                config.headers.Authorization = `Bearer ${token}`
            }
            return config
        },
        (error) => {
            isLoading.value = false
            Promise.reject(error)
        },
    )

    instance.interceptors.response.use(
        (response) => {
            isLoading.value = false
            const contentType = response.headers['content-type'] || ''
            if (contentType.includes('application/json') && response.data?.code !== 200) {
                return Promise.reject(response.data)
            }
            return response.data
        },
        async (error) => {
            isLoading.value = false

            const { response, config: originalRequest } = error
            if (response?.status === 500) {
                return Promise.reject(error)
            }

            if ([401, 403].includes(response?.status) && !originalRequest._retry) {
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
                    const newToken = await refreshTokenHandler()
                    originalRequest.headers.Authorization = `Bearer ${newToken}`
                    return instance(originalRequest)
                } catch (refreshError) {
                    return Promise.reject(refreshError)
                }
            }

            return Promise.reject(error)
        },
    )

    const baseService = {
        healthCheck: () => instance.get('/health-check'),
        create: (data) => instance.post('', data),
        getAll: () => instance.get(),
        delete: (id) => instance.delete(id),
        update: (data) => instance.put('', data),
    }

    return {
        isLoading,
        instance,
        baseService,
    }
}

export default useAxios
