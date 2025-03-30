<template>
    <div>
        <!-- Loop through posts -->
        <FeedPost
            v-for="post in posts"
            :key="post.id"
            :id="post.id"
            :content="post.content"
            :imageUrls="post.imageUrls"
            :creator="post.creator"
            :createdAt="post.createdAt"
            :stats="post.stats"
            :userActions="post.userActions"
            class="mb-4"
        />
        <ImageViewer />
    </div>
</template>

<script setup>
import { computed, onMounted, ref } from 'vue'
import FeedPost from './FeedPost.vue'
import { usePostStore } from '@/stores/postStore'
import ImageViewer from '../common/ImageViewer.vue'

const postStore = usePostStore()
const posts = computed(() => postStore.posts)

onMounted(() => {
    postStore.initPosts()
})
</script>

<style scoped></style>
