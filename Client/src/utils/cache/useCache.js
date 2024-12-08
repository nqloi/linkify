import { ref } from 'vue'
import { localStorageProvider } from './providers/localStorageProvider'

const useCache = (storageProvider = localStorageProvider) => {
  const storage = ref(storageProvider)

  const setCache = (key, value) => {
    storage.value.setItem(key, JSON.stringify(value))
  }

  const getCache = (key) => {
    const item = storage.value.getItem(key)
    return item ? JSON.parse(item) : null
  }

  const removeCache = (key) => {
    storage.value.removeItem(key)
  }

  return {
    setCache,
    getCache,
    removeCache,
    setStorageProvider: (newProvider) => (storage.value = newProvider)
  }
}

export default useCache
