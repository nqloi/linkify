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
                <Avatar
                    image="https://primefaces.org/cdn/primevue/images/avatar/amyelsner.png"
                    shape="circle"
                />
                <span class="font-bold whitespace-nowrap">Amy Elsner</span>
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
            <div class="flex items-center justify-between mt-4">
                <div class="flex gap-2">
                    <FileUpload
                        mode="basic"
                        chooseLabel="Image/Video"
                        chooseIcon="pi pi-images"
                        @select="onFileSelect"
                        :multiple="true"
                        auto
                        accept="image/*"
                        customUpload
                        severity="secondary"
                        class="p-button-outlined"
                        :maxFileSize="1000000"
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
import { Avatar, Textarea } from 'primevue'
import { ref, watch } from 'vue'
import FileUpload from 'primevue/fileupload'

const loading = useLoadingStore()
const postContent = ref('')
const visible = ref(false)
const header = 'Create Post'
const postStore = usePostStore()
const formData = new FormData()

const openDialog = () => {
    visible.value = true
}

const closeDialog = () => {
    visible.value = false
}

const handleCreatePost = async () => {
    formData.append('content', postContent.value)

    loading.show()
    await postStore.addPost(formData)
    loading.hide()
    visible.value = false
}

const onFileSelect = async (event) => {
    const { files = [] } = event ?? {}
    for (let i = 0; i < files.length; i++) {
        formData.append('images', files[i])
    }
}

const resetForm = () => {
    postContent.value = ''
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
