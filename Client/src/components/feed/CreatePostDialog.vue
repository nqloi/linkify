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
                    <Button label="Image/Video" severity="secondary" icon="pi pi-image" class="" />
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
import { ref } from 'vue'

const loading = useLoadingStore()
const postContent = ref('')
const visible = ref(false) // Trạng thái mở/đóng dialog
const header = 'Create Post' // Tiêu đề của dialog
const postStore = usePostStore()

const openDialog = () => {
    visible.value = true
}

const closeDialog = () => {
    visible.value = false
}

const handleCreatePost = async () => {
    loading.show()
    postStore.addPost({
        id: 123,
        userName: 'NQLoi',
        userAvatar: 'path_to_avatar2',
        postDate: 'Recent',
        postImage: 'path_to_post_image2',
        postContent: 'Another cool thing to share...',
        reactions: { likes: 50, comments: 18, shares: 7 },
    })
    setTimeout(() => {
        loading.hide()
        visible.value = false
    }, 1000)
}

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
