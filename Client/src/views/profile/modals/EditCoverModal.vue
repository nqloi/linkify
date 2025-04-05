<template>
    <Dialog
        :visible="isVisible"
        @update:visible="$emit('update:visible', $event)"
        :header="'Change Cover Photo'"
        :modal="true"
        class="w-full max-w-4xl"
    >
        <div class="p-fluid">
            <div class="mb-4">
                <div
                    class="cover-preview relative h-64 bg-gray-100 rounded-lg overflow-hidden"
                    :class="{ 'border-2 border-dashed border-gray-300': !selectedFile }"
                    @click="handlePreviewClick"
                    @dragover.prevent="handleDragOver"
                    @dragleave.prevent="isDragging = false"
                    @drop.prevent="handleDrop"
                >
                    <!-- Preview image with focal point control -->
                    <div v-if="previewUrl" class="relative w-full h-full">
                        <img
                            ref="previewImage"
                            :src="previewUrl"
                            class="w-full h-full object-cover transition-transform duration-300"
                            :style="{ objectPosition: focalPoint }"
                            alt="Cover preview"
                            @load="handleImageLoad"
                        />
                        <div
                            v-if="isDraggable"
                            class="absolute inset-0 bg-black bg-opacity-40 cursor-move"
                            @mousedown="startDragging"
                            @mousemove="handleDragging"
                            @mouseup="stopDragging"
                            @mouseleave="stopDragging"
                        >
                            <div class="text-white text-center mt-4">
                                <i class="pi pi-arrows-alt text-2xl"></i>
                                <p class="text-sm mt-1 select-none">Drag to adjust position</p>
                            </div>
                        </div>
                    </div>

                    <!-- Current cover photo -->
                    <div v-else-if="props.currentCoverUrl" class="w-full h-full">
                        <img
                            :src="props.currentCoverUrl"
                            class="w-full h-full object-cover"
                            :style="{ objectPosition: currentPosition }"
                            alt="Current cover"
                        />
                        <div
                            class="absolute inset-0 bg-black bg-opacity-40 flex flex-col items-center justify-center opacity-0 hover:opacity-100 transition-opacity duration-300"
                        >
                            <i class="pi pi-camera text-white text-2xl mb-2"></i>
                            <span class="text-white text-sm">Change cover photo</span>
                        </div>
                    </div>

                    <!-- Empty state -->
                    <div v-else class="flex flex-col items-center justify-center h-full">
                        <i class="pi pi-image text-4xl text-gray-400 mb-2"></i>
                        <span class="text-gray-500">Upload a cover photo</span>
                        <span class="text-sm text-gray-400 mt-2">
                            Recommended size: 820x312 pixels
                        </span>
                    </div>

                    <!-- Drag overlay -->
                    <div
                        v-if="isDragging"
                        class="absolute inset-0 bg-primary bg-opacity-20 border-2 border-dashed border-primary-500 flex items-center justify-center z-50"
                    >
                        <span class="text-primary-700 font-medium">Drop image here</span>
                    </div>

                    <input
                        type="file"
                        ref="fileInput"
                        class="hidden"
                        accept="image/jpeg,image/png"
                        @change="handleFileSelect"
                    />
                </div>

                <!-- Error message -->
                <div v-if="fileError" class="mt-2 text-center">
                    <Message severity="error" :closable="false">{{ fileError }}</Message>
                </div>

                <!-- File info -->
                <div v-if="selectedFile" class="mt-2 text-center text-sm text-gray-500">
                    {{ selectedFile.name }} ({{ formatFileSize(selectedFile.size) }})
                </div>
            </div>

            <div class="flex justify-center gap-4">
                <Button label="Upload Photo" icon="pi pi-upload" @click="triggerFileInput" />
                <Button
                    v-if="selectedFile && isDraggable"
                    :label="isDraggable ? 'Save Position' : 'Adjust Position'"
                    icon="pi pi-arrows-alt"
                    @click="toggleDragging"
                />
                <Button
                    v-if="hasExistingCover"
                    label="Remove"
                    icon="pi pi-trash"
                    severity="danger"
                    outlined
                    @click="handleRemove"
                />
            </div>
        </div>

        <template #footer>
            <div class="flex justify-end gap-2">
                <Button label="Cancel" icon="pi pi-times" @click="closeModal" outlined />
                <Button
                    label="Save"
                    icon="pi pi-check"
                    @click="handleSubmit"
                    :loading="loading"
                    :disabled="!selectedFile || !!fileError"
                />
            </div>
        </template>
    </Dialog>
</template>

<script setup>
import { ref, computed } from 'vue'
import Message from 'primevue/message'
import { useCustomToast } from '@/utils/toast/customToast'

const props = defineProps({
    visible: Boolean,
    currentCoverUrl: String,
    currentPosition: {
        type: String,
        default: '50% 50%',
    },
})

const emit = defineEmits(['update:visible', 'save', 'remove'])

const toast = useCustomToast()
const loading = ref(false)
const fileInput = ref(null)
const previewImage = ref(null)
const selectedFile = ref(null)
const previewUrl = ref('')
const fileError = ref('')
const isDragging = ref(false)
const isDraggable = ref(false)
const isPositionDragging = ref(false)
const dragStartPos = ref({ x: 0, y: 0 })
const currentOffset = ref({ x: 50, y: 50 })

// Computed property for visible prop
const isVisible = computed({
    get: () => props.visible,
    set: (value) => emit('update:visible', value),
})

const hasExistingCover = computed(() => {
    return !!props.currentCoverUrl && props.currentCoverUrl !== '/default-cover.jpg'
})

const focalPoint = computed(() => {
    return `${currentOffset.value.x}% ${currentOffset.value.y}%`
})

const triggerFileInput = () => {
    fileInput.value?.click()
}

const handlePreviewClick = () => {
    if (!isPositionDragging.value && !isDraggable.value) {
        triggerFileInput()
    }
}

const resetState = () => {
    selectedFile.value = null
    previewUrl.value = ''
    fileError.value = ''
    isDraggable.value = false
    isPositionDragging.value = false
    currentOffset.value = { x: 50, y: 50 }
}

const validateFile = (file) => {
    // Check if file exists
    if (!file) return 'Please select a file'

    // Check file type
    const allowedTypes = ['image/jpeg', 'image/png']
    if (!allowedTypes.includes(file.type)) {
        return 'Only JPG and PNG images are allowed'
    }

    // Check file size (max 10MB)
    const maxSize = 10 * 1024 * 1024 // 10MB
    if (file.size > maxSize) {
        return 'File size must be less than 10MB'
    }

    return null
}

const validateImageDimensions = (img) => {
    const minWidth = 820
    const minHeight = 312

    return new Promise((resolve) => {
        if (img.naturalWidth < minWidth || img.naturalHeight < minHeight) {
            resolve(
                `Image dimensions are too small. Minimum size is ${minWidth}x${minHeight} pixels.`,
            )
        }
        resolve(null)
    })
}

const handleImageLoad = async () => {
    if (previewImage.value) {
        const error = await validateImageDimensions(previewImage.value)
        if (error) {
            fileError.value = error
            selectedFile.value = null
            previewUrl.value = ''
            return
        }
        isDraggable.value = true
    }
}

const handleFileSelect = async (event) => {
    const file = event.target.files[0]
    if (!file) return

    // Validate file
    const error = validateFile(file)
    if (error) {
        fileError.value = error
        resetState()
        return
    }

    // Reset previous state before setting new file
    resetState()

    fileError.value = ''
    selectedFile.value = file
    previewUrl.value = URL.createObjectURL(file)
}

const handleDragOver = () => {
    isDragging.value = true
}

const handleDrop = (event) => {
    isDragging.value = false
    const file = event.dataTransfer.files[0]
    if (!file) return

    // Validate file
    const error = validateFile(file)
    if (error) {
        fileError.value = error
        resetState()
        return
    }

    // Reset previous state before setting new file
    resetState()

    fileError.value = ''
    selectedFile.value = file
    previewUrl.value = URL.createObjectURL(file)
}

const formatFileSize = (bytes) => {
    if (bytes < 1024) return bytes + ' bytes'
    else if (bytes < 1048576) return (bytes / 1024).toFixed(1) + ' KB'
    else return (bytes / 1048576).toFixed(1) + ' MB'
}

const toggleDragging = () => {
    isDraggable.value = !isDraggable.value
}

const startDragging = (event) => {
    if (!isDraggable.value) return
    isPositionDragging.value = true
    dragStartPos.value = {
        x: event.clientX,
        y: event.clientY,
    }
}

const handleDragging = (event) => {
    if (!isPositionDragging.value) return

    const deltaX = event.clientX - dragStartPos.value.x
    const deltaY = event.clientY - dragStartPos.value.y

    // Update position with boundaries (0-100)
    currentOffset.value = {
        x: Math.max(0, Math.min(100, currentOffset.value.x + deltaX * 0.1)),
        y: Math.max(0, Math.min(100, currentOffset.value.y + deltaY * 0.1)),
    }

    dragStartPos.value = {
        x: event.clientX,
        y: event.clientY,
    }
}

const stopDragging = () => {
    isPositionDragging.value = false
}

const handleRemove = async () => {
    try {
        loading.value = true
        await emit('remove')
        toast.showSuccess('Cover photo removed successfully')
        closeModal()
    } catch (error) {
        toast.showError('Failed to remove cover photo')
        console.error('Failed to remove cover:', error)
    } finally {
        loading.value = false
    }
}

const closeModal = () => {
    resetState()
    emit('update:visible', false)
}

const handleSubmit = async () => {
    if (!selectedFile.value) return

    // Final validation before submit
    const error = validateFile(selectedFile.value)
    if (error) {
        fileError.value = error
        return
    }

    try {
        loading.value = true
        await emit('save', {
            file: selectedFile.value,
            position: focalPoint.value,
        })
        toast.showSuccess('Cover photo updated successfully')
        closeModal()
    } catch (error) {
        toast.showError('Failed to update cover photo')
        console.error('Failed to save cover:', error)
    } finally {
        loading.value = false
    }
}
</script>

<style scoped>
.cover-preview {
    transition: all 0.3s ease;
    cursor: pointer;
}

.cover-preview:hover {
    border-color: var(--primary-color);
}

/* Draggable image styles */
.cover-preview img {
    pointer-events: none;
}

.cover-preview .cursor-move {
    cursor: move;
}
</style>
