<template>
    <SimpleCard class="post-input p-4">
        <!-- Input row -->
        <div class="flex items-center gap-4 mb-4">
            <Avatar icon="pi pi-user" shape="circle" class="w-10 h-10 bg-blue-500 text-white" />
            <InputText
                v-model="postContent"
                placeholder="What's on your mind?"
                class="flex-1 rounded-full p-inputtext-sm"
            />
            <Button
                label="Share Post"
                class="p-button-sm p-button-rounded"
                :disabled="!postContent"
            />
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
</template>

<script setup>
import { ref } from 'vue'
import { Avatar } from 'primevue'
import SimpleCard from '../common/SimpleCard.vue'

// State management for the post input
const postContent = ref('')

// Visibility options for the post
const visibility = ref(null)
const visibilityOptions = ref([
    { label: 'Public', value: 'Public' },
    { label: 'Friends', value: 'Friends' },
    { label: 'Only Me', value: 'Only Me' },
])
</script>

<style scoped>
.post-input {
    width: 100%;
}

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
