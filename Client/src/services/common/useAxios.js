import Cookies from 'js-cookie'
import axios from 'axios'
import { ref } from 'vue'

const defaultVersion = 'v1'
let isRefreshing = false
let failedQueue = []

const processQueue = (error, token = null) => {
  failedQueue.forEach(({ resolve, reject }) => {
    error ? reject(error) : resolve(token)
  })
  failedQueue = []
}

const redirectToLogin = () => {
  // Xóa token cũ
  // Cookies.remove('accessToken')
  // Cookies.remove('refreshToken')
  // Chuyển hướng người dùng tới trang đăng nhập
  // window.location.href = '/'
}

const useAxios = (controller, version = defaultVersion, customHeader = {}) => {
  const isLoading = ref(false)

  const instance = axios.create({
    baseURL: `https://localhost:7029/api/${version}/${controller}`,
    headers: {
      accept: 'application/json',
      'Content-Type': 'application/json',
      ...customHeader
    }
  })

  const refreshTokenHandler = async () => {
    try {
      const refreshToken = Cookies.get('refreshToken')
      if (!refreshToken) throw new Error('No refresh token available')

      const response = await instance.post(
        'https://localhost:7029/api/v1/auth/refresh-token',
        refreshToken
      )
      const { accessToken } = response.content

      if (!accessToken) throw new Error('Failed to refresh access token')

      Cookies.set('accessToken', accessToken)
      processQueue(null, accessToken)
      return accessToken
    } catch (error) {
      processQueue(error)
      redirectToLogin()
      throw error
    } finally {
      isRefreshing = false
    }
  }

  instance.interceptors.request.use(
    (config) => {
      isLoading.value = true
      const token = Cookies.get('accessToken')
      if (token) {
        config.headers.Authorization = `Bearer ${token}`
      }
      return config
    },
    (error) => {
      isLoading.value = false
      Promise.reject(error)
    }
  )

  instance.interceptors.response.use(
    (response) => {
      isLoading.value = false
      const contentType = response.headers['content-type'] || ''
      if (contentType.includes('application/json') && response.data?.code !== 200) {
        return Promise.reject(
          new Error(
            `${response.data?.code}. ${response.data?.message}. innerException: ${
              response.data?.error?.innerException || 'No inner exception'
            }`
          )
        )
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
    }
  )

  const baseService = {
    healthCheck: () => instance.get('/health-check')
  }

  return {
    isLoading,
    instance,
    baseService
  }
}

export default useAxios
