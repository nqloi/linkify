<template>
    <div>
        <!-- Loop through posts -->
        <FeedPost v-for="post in posts" :key="post.id" :post="post" class="mb-4" />
    </div>
</template>

<script setup>
import { ref } from 'vue'
import FeedPost from './FeedPost.vue'

// Fake dữ liệu bài đăng
const posts = ref([
    {
        id: 1,
        userName: 'Cameron Williamson',
        userAvatar: 'path_to_avatar1',
        postDate: '23 Aug at 4:21 PM',
        postImage: 'path_to_post_image1',
        postContent: 'Something interesting about the post...',
        reactions: { likes: 30, comments: 12, shares: 5 },
    },
    {
        id: 2,
        userName: 'Terry Lipshutz',
        userAvatar: 'path_to_avatar2',
        postDate: '22 Aug at 7:15 PM',
        postImage: 'path_to_post_image2',
        postContent: 'Another cool thing to share...',
        reactions: { likes: 50, comments: 18, shares: 7 },
    },
    {
        id: 3,
        userName: 'Terry Lipshutz',
        userAvatar: 'path_to_avatar2',
        postDate: '22 Aug at 7:15 PM',
        postImage: 'path_to_post_image2',
        postContent: 'Another cool thing to share...',
        reactions: { likes: 50, comments: 18, shares: 7 },
    },
    {
        id: 4,
        userName: 'Terry Lipshutz',
        userAvatar: 'path_to_avatar2',
        postDate: '22 Aug at 7:15 PM',
        postImage: 'path_to_post_image2',
        postContent: 'Another cool thing to share...',
        reactions: { likes: 50, comments: 18, shares: 7 },
    },
    {
        id: 5,
        userName: 'Terry Lipshutz',
        userAvatar: 'path_to_avatar2',
        postDate: '22 Aug at 7:15 PM',
        postImage: 'path_to_post_image2',
        postContent: 'Another cool thing to share...',
        reactions: { likes: 50, comments: 18, shares: 7 },
    },
])
</script>

<style scoped></style>
