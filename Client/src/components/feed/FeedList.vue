<template>
    <div>
        <!-- Loop through posts -->
        <FeedPost v-for="post in posts" :key="post.id" :post="post" class="mb-4" />
    </div>
</template>

<script setup>
import { computed, onMounted, ref } from 'vue'
import FeedPost from './FeedPost.vue'
import { usePostStore } from '@/stores/postStore'

const postStore = usePostStore()
const posts = computed(() => postStore.posts)

onMounted(() => {
    postStore.initPosts()
})
</script>

<style scoped></style>
