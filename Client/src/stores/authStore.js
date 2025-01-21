import useAuthService from '@/services/authService'
import { CACHE_KEYS } from '@/utils/cache/cacheContants'
import useCache from '@/utils/cache/useCache'
import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useAuthStore = defineStore(
    'auth',
    () => {
        const defaultUser = {
            userId: null,
            userName: null,
            firstName: null,
            lastName: null,
            avatarUrl: null,
        }
        const authService = useAuthService()
        const { setCache, removeCache } = useCache()

        const isAuthenticated = ref(false)
        const isSessionTimeout = ref(false)
        const user = ref(defaultUser)

        const login = async (credentials) => {
            const response = await authService.login(credentials)

            const { accessToken, refreshToken, ...userInfos } = response.content
            setCache(CACHE_KEYS.ACCESS_TOKEN, accessToken)
            setCache(CACHE_KEYS.REFRESH_TOKEN, refreshToken)

            isAuthenticated.value = true
            isSessionTimeout.value = false
            user.value = userInfos

            return true
        }

        const logout = () => {
            // await authService.logout()
            removeCache(CACHE_KEYS.ACCESS_TOKEN)
            removeCache(CACHE_KEYS.REFRESH_TOKEN)
            Object.assign(user, { ...defaultUser })
            isAuthenticated.value = false
        }

        const onSessionTimeout = () => {
            isAuthenticated.value = false
            isSessionTimeout.value = true
            removeCache(CACHE_KEYS.ACCESS_TOKEN)
            removeCache(CACHE_KEYS.REFRESH_TOKEN)
            Object.assign(user, { ...defaultUser })
        }

        const getDisplayName = () => user.value.firstName + ' ' + user.value.lastName

        return {
            isAuthenticated,
            user,
            login,
            logout,
            isSessionTimeout,
            onSessionTimeout,
            getDisplayName,
        }
    },
    {
        persist: true,
    },
)
