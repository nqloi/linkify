import useAuthService from '@/services/authService'
import { CACHE_KEYS } from '@/utils/cache/cacheConstants'
import useCache from '@/utils/cache/useCache'
import { defineStore } from 'pinia'
import { ref } from 'vue'
import { useNotificationStore } from '@/stores/notificationStore'
import { logger } from '@/utils/logger'

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

        const logout = async () => {
            try {
                // Clean up notifications before logging out
                const notificationStore = useNotificationStore()
                await notificationStore.resetState()

                await authService.logout()
                removeCache(CACHE_KEYS.ACCESS_TOKEN)
                removeCache(CACHE_KEYS.REFRESH_TOKEN)
                Object.assign(user.value, { ...defaultUser })
                isAuthenticated.value = false
            } catch (error) {
                logger.error('Failed to logout', { error })
                throw error
            }
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
            isSessionTimeout,
            user,
            login,
            logout,
            onSessionTimeout,
            getDisplayName,
        }
    },
    {
        persist: true,
    },
)
