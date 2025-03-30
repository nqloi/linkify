import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useImageViewerStore = defineStore('imageViewer', () => {
    const visible = ref(false)
    const imageUrls = ref([])
    const initialIndex = ref(0)

    const openViewer = (urls, index = 0) => {
        imageUrls.value = urls
        initialIndex.value = index
        visible.value = true
    }

    const closeViewer = () => {
        visible.value = false
    }

    return {
        visible,
        imageUrls,
        initialIndex,
        openViewer,
        closeViewer,
    }
})
