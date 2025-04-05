<template>
    <Dialog
        :visible="isVisible"
        @update:visible="$emit('update:visible', $event)"
        :header="'Change Profile Picture'"
        :modal="true"
        class="w-full max-w-md"
    >
        <div class="p-fluid">
            <div class="mb-4">
                <div
                    class="avatar-preview relative w-48 h-48 mx-auto rounded-full overflow-hidden"
                    :class="{ 'border-2 border-dashed border-gray-300': !selectedFile }"
                    @dragover.prevent="handleDragOver"
                    @dragleave.prevent="isDragging = false"
                    @drop.prevent="handleDrop"
                >
                    <img
                        v-if="previewUrl"
                        :src="previewUrl"
                        class="w-full h-full object-cover"
                        alt="Avatar preview"
                    />
                    <div v-else-if="props.currentAvatarUrl" class="w-full h-full">
                        <img
                            :src="props.currentAvatarUrl"
                            class="w-full h-full object-cover"
                            alt="Current avatar"
                        />
                        <div
                            class="absolute inset-0 bg-black bg-opacity-40 flex flex-col items-center justify-center opacity-0 hover:opacity-100 transition-opacity duration-300"
                        >
                            <i class="pi pi-camera text-white text-2xl mb-2"></i>
                            <span class="text-white text-sm">Change photo</span>
                        </div>
                    </div>
                    <div v-else class="flex flex-col items-center justify-center h-full">
                        <i class="pi pi-user text-4xl text-gray-400 mb-2"></i>
                        <span class="text-gray-500">Upload a photo</span>
                    </div>

                    <!-- Drag overlay -->
                    <div
                        v-if="isDragging"
                        class="absolute inset-0 bg-primary bg-opacity-20 border-2 border-dashed border-primary-500 flex items-center justify-center"
                    >
                        <span class="text-primary-700 font-medium">Drop image here</span>
                    </div>

                    <input
                        type="file"
                        ref="fileInput"
                        class="hidden"
                        accept="image/jpeg,image/png,image/gif"
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
                    v-if="hasExistingAvatar"
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
import Dialog from 'primevue/dialog'
import Button from 'primevue/button'
import Message from 'primevue/message'
import { useCustomToast } from '@/utils/toast/customToast'

const props = defineProps({
    visible: Boolean,
    currentAvatarUrl: String,
})

const emit = defineEmits(['update:visible', 'save', 'remove'])

const toast = useCustomToast()
const loading = ref(false)
const fileInput = ref(null)
const selectedFile = ref(null)
const previewUrl = ref('')
const fileError = ref('')
const isDragging = ref(false)

// Computed property for visible prop
const isVisible = computed({
    get: () => props.visible,
    set: (value) => emit('update:visible', value),
})

const hasExistingAvatar = computed(() => {
    return !!props.currentAvatarUrl && props.currentAvatarUrl !== '/default-avatar.jpg'
})

const triggerFileInput = () => {
    fileInput.value.click()
}

const validateFile = (file) => {
    // Check if file exists
    if (!file) return 'Please select a file'

    // Check file type
    const allowedTypes = ['image/jpeg', 'image/png', 'image/gif']
    if (!allowedTypes.includes(file.type)) {
        return 'Only JPG, PNG and GIF images are allowed'
    }

    // Check file size (max 5MB)
    const maxSize = 5 * 1024 * 1024 // 5MB
    if (file.size > maxSize) {
        return 'File size must be less than 5MB'
    }

    return null
}

const handleFileSelect = (event) => {
    const file = event.target.files[0]
    if (!file) return

    // Validate file
    const error = validateFile(file)
    if (error) {
        fileError.value = error
        selectedFile.value = null
        previewUrl.value = ''
        return
    }

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
        selectedFile.value = null
        previewUrl.value = ''
        return
    }

    fileError.value = ''
    selectedFile.value = file
    previewUrl.value = URL.createObjectURL(file)
}

const formatFileSize = (bytes) => {
    if (bytes < 1024) return bytes + ' bytes'
    else if (bytes < 1048576) return (bytes / 1024).toFixed(1) + ' KB'
    else return (bytes / 1048576).toFixed(1) + ' MB'
}

const handleRemove = async () => {
    try {
        loading.value = true
        await emit('remove')
        toast.showSuccess('Profile picture removed successfully')
        closeModal()
    } catch (error) {
        toast.showError('Failed to remove profile picture')
        console.error('Failed to remove avatar:', error)
    } finally {
        loading.value = false
    }
}

const closeModal = () => {
    selectedFile.value = null
    previewUrl.value = ''
    fileError.value = ''
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
        await emit('save', selectedFile.value)
        toast.showSuccess('Profile picture updated successfully')
        closeModal()
    } catch (error) {
        toast.showError('Failed to update profile picture')
        console.error('Failed to save avatar:', error)
    } finally {
        loading.value = false
    }
}
</script>

<style scoped>
.avatar-preview {
    transition: all 0.3s ease;
    cursor: pointer;
}

.avatar-preview:hover {
    border-color: var(--primary-color);
}
</style>
