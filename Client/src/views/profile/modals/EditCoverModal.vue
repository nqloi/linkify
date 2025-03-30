<template>
    <Dialog
        v-model:visible="visible"
        :header="'Change Cover Photo'"
        :modal="true"
        class="w-full max-w-2xl"
    >
        <div class="p-fluid">
            <div class="mb-4">
                <div
                    class="cover-preview relative h-48 bg-gray-100 rounded-lg overflow-hidden"
                    :class="{ 'border-2 border-dashed border-gray-300': !selectedFile }"
                >
                    <img
                        v-if="previewUrl"
                        :src="previewUrl"
                        class="w-full h-full object-cover"
                        alt="Cover preview"
                    />
                    <div v-else class="flex flex-col items-center justify-center h-full">
                        <i class="pi pi-image text-4xl text-gray-400 mb-2"></i>
                        <span class="text-gray-500">Upload a cover photo</span>
                    </div>

                    <input
                        type="file"
                        ref="fileInput"
                        class="hidden"
                        accept="image/*"
                        @change="handleFileSelect"
                    />
                </div>
            </div>

            <div class="flex justify-center gap-4">
                <Button label="Upload Photo" icon="pi pi-upload" @click="triggerFileInput" />
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
                    :disabled="!selectedFile"
                />
            </div>
        </template>
    </Dialog>
</template>

<script setup>
import { ref, computed } from 'vue'
import Dialog from 'primevue/dialog'
import Button from 'primevue/button'

const props = defineProps({
    visible: Boolean,
    currentCoverUrl: String,
})

const emit = defineEmits(['update:visible', 'save', 'remove'])

const loading = ref(false)
const fileInput = ref(null)
const selectedFile = ref(null)
const previewUrl = ref('')

const hasExistingCover = computed(() => {
    return !!props.currentCoverUrl && props.currentCoverUrl !== '/default-cover.jpg'
})

const triggerFileInput = () => {
    fileInput.value.click()
}

const handleFileSelect = (event) => {
    const file = event.target.files[0]
    if (file) {
        selectedFile.value = file
        previewUrl.value = URL.createObjectURL(file)
    }
}

const handleRemove = async () => {
    try {
        loading.value = true
        await emit('remove')
        closeModal()
    } catch (error) {
        console.error('Failed to remove cover:', error)
    } finally {
        loading.value = false
    }
}

const closeModal = () => {
    selectedFile.value = null
    previewUrl.value = ''
    emit('update:visible', false)
}

const handleSubmit = async () => {
    if (!selectedFile.value) return

    try {
        loading.value = true
        await emit('save', selectedFile.value)
        closeModal()
    } catch (error) {
        console.error('Failed to save cover:', error)
    } finally {
        loading.value = false
    }
}
</script>
