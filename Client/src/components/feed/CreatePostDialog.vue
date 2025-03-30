<template>
    <Dialog
        v-model:visible="visible"
        :header="header"
        modal
        :breakpoints="{ '960px': '95vw' }"
        class="create-post-dialog"
    >
        <template #header>
            <div class="inline-flex items-center justify-center gap-2">
                <UserAvatar :size="SIZE.SM" :user="user" />
                <span class="font-bold whitespace-nowrap">{{ getDisplayName() }}</span>
            </div>
        </template>
        <div class="post-form">
            <Textarea
                autofocus
                v-model="postContent"
                rows="5"
                fluid
                placeholder="What's on your mind?"
            ></Textarea>
            <div class="flex flex-wrap gap-4">
                <div
                    v-for="(file, index) of fileUploads"
                    :key="file.name + file.type + file.size"
                    class="p-4 rounded-border flex flex-col border border-surface items-center gap-4"
                >
                    <div>
                        <img
                            role="presentation"
                            :alt="file.name"
                            :src="file.objectURL"
                            width="100"
                            height="50"
                        />
                    </div>
                    <span
                        class="font-semibold text-ellipsis max-w-60 whitespace-nowrap overflow-hidden"
                        >{{ file.name }}</span
                    >
                    <div>{{ formatFileSize(file.size) }}</div>
                    <Button
                        icon="pi pi-times"
                        @click="onRemoveTemplatingFile(file, index)"
                        outlined
                        rounded
                        severity="danger"
                    />
                </div>
            </div>
            <div class="flex items-center justify-between mt-4">
                <div class="flex gap-2">
                    <FileUpload
                        mode="basic"
                        chooseLabel="Image/Video"
                        chooseIcon="pi pi-images"
                        :multiple="true"
                        auto
                        accept="image/*"
                        customUpload
                        severity="secondary"
                        class="p-button-outlined"
                        :maxFileSize="1000000"
                        @select="onFileSelect"
                    />
                    <Button
                        label="Attachment"
                        severity="secondary"
                        icon="pi pi-paperclip"
                        class=""
                    />
                    <Button label="Live" severity="secondary" icon="pi pi-video" class="" />
                    <Button label="Hashtag" severity="secondary" icon="pi pi-hashtag" class="" />
                    <Button label="Mention" severity="secondary" icon="pi pi-at" class="" />
                </div>
            </div>
        </div>
        <template #footer>
            <Button label="Share" @click="handleCreatePost" />
        </template>
    </Dialog>
</template>

<script setup>
import { useLoadingStore } from '@/stores/loadingStore'
import { usePostStore } from '@/stores/postStore'
import { Textarea } from 'primevue'
import { ref, watch } from 'vue'
import FileUpload from 'primevue/fileupload'
import { SIZE_UNITS, SIZE_BASE } from '@/common/constants/file'
import { useCustomToast } from '@/utils/toast/customToast'
import UserAvatar from '../common/UserAvatar.vue'
import { useAuthStore } from '@/stores/authStore'
import { SIZE } from '@/common/constants/size'

const toast = useCustomToast()
const MAX_IMAGE_LENGTH = 5
const loading = useLoadingStore()
const postContent = ref('')
const visible = ref(false)
const header = 'Create Post'
const postStore = usePostStore()
const formData = new FormData()
const fileUploads = ref([])

// store
const { user, getDisplayName } = useAuthStore()

const openDialog = () => {
    visible.value = true
}

const closeDialog = () => {
    visible.value = false
}

const handleCreatePost = async () => {
    formData.append('content', postContent.value)
    fileUploads.value.forEach((file) => formData.append('images', file))

    loading.show()
    await postStore.addPost(formData)
    loading.hide()
    visible.value = false
}

const onFileSelect = async (event) => {
    const { files = [] } = event ?? {}
    if (files.length > MAX_IMAGE_LENGTH) {
        toast.showCustomError(
            'You can select up to 5 images only. Please remove some and try again!',
        )
        return
    }
    for (let i = 0; i < files.length; i++) {
        fileUploads.value.push(files[i])
    }
}

const resetForm = () => {
    postContent.value = ''
    fileUploads.value = []
}

const onRemoveTemplatingFile = (file, index) => {
    fileUploads.value.splice(index, 1)
}

const formatFileSize = (sizeInBytes) => {
    if (sizeInBytes === 0) return '0 Bytes'

    const unitIndex = Math.floor(Math.log(sizeInBytes) / Math.log(SIZE_BASE))
    const formattedSize = (sizeInBytes / Math.pow(SIZE_BASE, unitIndex)).toFixed(2)
    return `${formattedSize} ${SIZE_UNITS[unitIndex]}`
}

watch(visible, () => {
    !visible.value && resetForm()
})

defineExpose({ openDialog, closeDialog })
</script>

<style scoped>
.create-post-dialog {
    border-radius: 10px;
    overflow: hidden;
}

.p-dialog-header {
    background-color: #f8f9fa;
    color: #333;
    font-weight: bold;
    text-align: center;
}

.p-dialog-content {
    padding: 1.5rem;
}

.post-form textarea {
    resize: none;
}
</style>
