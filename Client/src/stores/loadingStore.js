import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useLoadingStore = defineStore('loading', () => {
    const visible = ref(false)
    const message = ref('')

    const show = (msg = '', timeout = 30000) => {
        visible.value = true
        message.value = msg

        setTimeout(() => {
            visible.value = false
        }, timeout)
    }

    const hide = () => {
        visible.value = false
        message.value = ''
    }

    return { visible, message, show, hide }
})
