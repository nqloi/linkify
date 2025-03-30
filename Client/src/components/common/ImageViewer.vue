<template>
    <div
        v-if="store.visible"
        class="flex items-center justify-center w-screen h-screen fixed top-0 left-0 bg-black bg-opacity-90 z-50"
        @keydown="handleKeydown"
        tabindex="0"
    >
        <!-- Download button -->
        <Button
            icon="pi pi-download"
            severity="secondary"
            class="absolute top-4 right-20"
            rounded
            v-tooltip.left="'Download'"
            @click="downloadImage"
        />

        <!-- Copy button -->
        <Button
            icon="pi pi-copy"
            severity="secondary"
            class="absolute top-4 right-36"
            rounded
            v-tooltip.left="'Copy'"
            @click="copyImage"
        />

        <!-- Close button -->
        <Button
            icon="pi pi-times"
            severity="secondary"
            class="absolute top-4 right-4"
            rounded
            v-tooltip="'Press Esc to close'"
            @click="closeViewer"
        />

        <!-- Previous button -->
        <Button
            icon="pi pi-chevron-left"
            severity="secondary"
            rounded
            class="w-12 h-12 flex-shrink-0 ml-6 mr-4"
            @click="prevImage"
        />

        <!-- Image display -->
        <div class="image-container flex justify-center items-center w-full h-full">
            <img
                :src="store.imageUrls[currentIndex]"
                alt="Image Preview"
                class="max-h-full max-w-full object-contain rounded-lg"
            />
        </div>

        <!-- Next button -->
        <Button
            icon="pi pi-chevron-right"
            class="w-12 h-12 flex-shrink-0 mr-6 ml-4"
            severity="secondary"
            rounded
            @click="nextImage"
        />
    </div>
</template>

<script setup>
import { useImageViewerStore } from '@/stores/imageViewerStore'
import { useCustomToast } from '@/utils/toast/customToast'
import { ref, onMounted, onUnmounted, watch } from 'vue'

const emit = defineEmits(['close', 'update:visible'])

const store = useImageViewerStore()
const toast = useCustomToast()

const currentIndex = ref(store.initialIndex)

const closeViewer = () => {
    store.closeViewer()
}

const prevImage = () => {
    currentIndex.value = (currentIndex.value - 1 + store.imageUrls.length) % store.imageUrls.length
}

const nextImage = () => {
    currentIndex.value = (currentIndex.value + 1) % store.imageUrls.length
}

const handleKeydown = (event) => {
    if (event.key === 'ArrowLeft') {
        prevImage()
    } else if (event.key === 'ArrowRight') {
        nextImage()
    } else if (event.key === 'Escape') {
        closeViewer()
    }
}

const downloadImage = async () => {
    const response = await fetch(store.imageUrls[currentIndex.value])
    const blob = await response.blob()
    const objectUrl = URL.createObjectURL(blob)

    const link = document.createElement('a')
    link.href = objectUrl
    link.setAttribute('download', `image-${currentIndex.value + 1}.jpg`)
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)

    // dispose
    URL.revokeObjectURL(objectUrl)
}

const copyImage = async () => {
    try {
        await navigator.clipboard.writeText(store.imageUrls[currentIndex.value])
        toast.showCustomSuccess('Url of image copied to clipboard')
    } catch (error) {
        console.error('Failed to copy image URL:', error)
        toast.showCustomError('Failed to copy')
    }
}

onMounted(() => {
    window.addEventListener('keydown', handleKeydown)
})

onUnmounted(() => {
    window.removeEventListener('keydown', handleKeydown)
})

watch(
    () => store.initialIndex,
    (newIndex) => {
        currentIndex.value = newIndex
    },
)

watch(
    () => store.visible,
    (newVal) => {
        if (newVal) {
            currentIndex.value = store.initialIndex
        }
    },
)
</script>

<style scoped></style>
