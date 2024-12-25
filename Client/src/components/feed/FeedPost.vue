<template>
    <div class="feed-post bg-white p-4 rounded-lg shadow-md">
        <!-- Header -->
        <div class="post-header flex items-center justify-between mb-4">
            <div class="flex items-center">
                <Avatar
                    :image="post.avatarUrl ?? defaultAvatar"
                    alt="User Avatar"
                    class="w-10 h-10 rounded-full mr-3"
                />
                <div>
                    <h5 class="font-bold text-sm">{{ post.userName }}</h5>
                    <p class="text-gray-500 text-xs">{{ post.postDate }}</p>
                </div>
            </div>
            <Button icon="pi pi-ellipsis-h" class="p-button-text" />
        </div>

        <!-- Content -->
        <div class="post-content mb-4">
            <p class="text-sm text-gray-700 mb-3">{{ post.content }}</p>
            <img :src="post.postImage" alt="Post Image" class="w-full rounded-lg" />
        </div>

        <!-- Footer (Reactions) -->
        <div class="post-footer flex items-center justify-between text-sm text-gray-600">
            <div class="flex items-center gap-4">
                <Button
                    icon="pi pi-thumbs-up"
                    label="Like"
                    class="p-button-text p-button-sm text-gray-600"
                />
                <Button
                    icon="pi pi-comment"
                    label="Comment"
                    class="p-button-text p-button-sm text-gray-600"
                />
                <Button
                    icon="pi pi-share-alt"
                    label="Share"
                    class="p-button-text p-button-sm text-gray-600"
                />
            </div>
            <p>
                {{ post.reactions?.likes }} Like • {{ post.reactions?.comments }} Comment •
                {{ post.reactions?.shares }} Share
            </p>
        </div>
    </div>
</template>

<script setup>
import { Avatar } from 'primevue'
import defaultAvatar from '@/assets/images/avatar-default.svg'

defineProps({
    post: {
        type: Object,
        required: true,
    },
})
</script>

<style scoped>
.feed-post {
    border: 1px solid #e5e7eb;
}

.post-header img {
    object-fit: cover;
}

.post-content img {
    max-height: 400px;
    object-fit: cover;
}
</style>
