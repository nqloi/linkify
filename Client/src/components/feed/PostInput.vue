<template>
    <div class="w-full">
        <SimpleCard class="p-4 mb-4">
            <!-- Input row -->
            <div class="flex items-center gap-4 mb-4">
                <Avatar icon="pi pi-user" shape="circle" class="w-10 h-10 bg-blue-500 text-white" />
                <div
                    class="flex-1 rounded-full p-input text-sm border border-inherit py-2 px-4 bg-slate-100 hover:bg-slate-200 cursor-pointer"
                    @click="handleOpenDialog"
                >
                    What's on your mind?
                </div>
            </div>

            <!-- Options row -->
            <div class="flex items-center justify-between text-sm text-gray-600">
                <div class="flex items-center gap-4">
                    <Button
                        icon="pi pi-image"
                        label="Image/Video"
                        class="p-button-text p-button-sm text-blue-500"
                    />
                    <Button
                        icon="pi pi-paperclip"
                        label="Attachment"
                        class="p-button-text p-button-sm text-orange-500"
                    />
                    <Button
                        icon="pi pi-video"
                        label="Live"
                        class="p-button-text p-button-sm text-red-500"
                    />
                    <Button
                        icon="pi pi-hashtag"
                        label="Hashtag"
                        class="p-button-text p-button-sm text-green-500"
                    />
                    <Button
                        icon="pi pi-at"
                        label="Mention"
                        class="p-button-text p-button-sm text-gray-600"
                    />
                </div>
                <Dropdown
                    v-model="visibility"
                    :options="visibilityOptions"
                    class="p-dropdown-sm"
                    placeholder="Public"
                />
            </div>
        </SimpleCard>
        <CreatePostDialog ref="postDialog" />
    </div>
</template>

<script setup>
import { Fragment, ref } from 'vue'
import { Avatar } from 'primevue'
import SimpleCard from '../common/SimpleCard.vue'
import CreatePostDialog from './CreatePostDialog.vue'

// State management for the post input
const postDialog = ref('')

// Visibility options for the post
const visibility = ref(null)
const visibilityOptions = ref([
    { label: 'Public', value: 'Public' },
    { label: 'Friends', value: 'Friends' },
    { label: 'Only Me', value: 'Only Me' },
])

const handleOpenDialog = () => {
    postDialog.value.openDialog()
}
</script>

<style scoped>
.post-input .p-inputtext {
    border: 1px solid #e5e7eb;
}

.post-input .p-button-sm {
    font-size: 0.85rem;
}

.post-input .p-dropdown {
    min-width: 100px;
}
</style>
