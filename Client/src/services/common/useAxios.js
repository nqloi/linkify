import axios from 'axios'
import { ref } from 'vue'
import { createAuthInterceptor } from './interceptors/authInterceptor'
import { createRefreshTokenInterceptor } from './interceptors/refreshTokenInterceptor'
import { createBaseService } from './baseServiceFactory'

const API_URL = 'https://localhost:7029/api'
const DEFAULT_VERSION = 'v1'

export const createAxiosConfig = ({
    controller,
    version = DEFAULT_VERSION,
    headers = {},
    auth = true,
    refreshToken = true,
}) => ({
    controller,
    version,
    headers: {
        accept: 'application/json',
        'Content-Type': 'application/json',
        ...headers,
    },
    auth,
    refreshToken,
})

const useAxios = (config) => {
    const { controller, version, headers, auth, refreshToken } =
        typeof config === 'string' ? createAxiosConfig({ controller: config }) : config

    const isLoading = ref(false)
    const instance = axios.create({
        baseURL: `${API_URL}/${version}/${controller}`,
        headers,
    })

    // Loading interceptors
    instance.interceptors.request.use(
        (config) => {
            isLoading.value = true
            return config
        },
        (error) => {
            isLoading.value = false
            return Promise.reject(error)
        },
    )

    instance.interceptors.response.use(
        (response) => {
            isLoading.value = false
            return response.data
        },
        (error) => {
            isLoading.value = false
            return Promise.reject(error)
        },
    )

    // Auth interceptor
    if (auth) {
        const authInterceptor = createAuthInterceptor()
        instance.interceptors.request.use(authInterceptor.request, authInterceptor.response)
    }

    // Refresh token interceptor
    if (refreshToken) {
        const refreshTokenInterceptor = createRefreshTokenInterceptor(
            instance,
            `${API_URL}/${version}/auth/refresh-token`,
        )
        instance.interceptors.response.use((response) => response, refreshTokenInterceptor.response)
    }

    return {
        isLoading,
        instance,
        baseService: createBaseService(instance),
    }
}

export default useAxios
