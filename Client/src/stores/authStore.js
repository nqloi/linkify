import { authService } from '@/services/authenService'
import Cookies from 'js-cookie'
import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useAuthStore = defineStore(
  'auth',
  () => {
    const defaultUser = {
      userId: null,
      userName: null,
      firstName: null,
      lastName: null
    }

    const isAuthenticated = ref(false)
    const user = ref(defaultUser)

    const login = async (credentials) => {
      const response = await authService.login(credentials)

      const { accessToken, refreshToken, ...userInfos } = response.content

      Cookies.set('accessToken', accessToken, { expires: 7, secure: true, sameSite: 'Strict' })
      Cookies.set('refreshToken', refreshToken, { expires: 30, secure: true, sameSite: 'Strict' })

      isAuthenticated.value = true
      user.value = userInfos

      return true
    }

    const logout = () => {
      Cookies.remove('accessToken')
      Cookies.remove('refreshToken')
      Object.assign(user, { ...defaultUser })
      isAuthenticated.value = false
    }

    const register = async () => {}

    return { isAuthenticated, user, login, logout }
  },
  {
    persist: true
  }
)
