<template>
    <div class="home overflow-auto flex-1 w-full flex flex-col">
        <StoryBar class="mb-4" />
        <PostInput class="mb-4" />
        <FeedList class="flex-1" />
    </div>
</template>
<script setup>
import FeedList from '@/components/feed/FeedList.vue'
import PostInput from '@/components/feed/PostInput.vue'
import StoryBar from '@/components/story/StoryBar.vue'
</script>
<style lang="scss">
.home {
    scrollbar-width: none;
    ::-webkit-scrollbar {
        display: none; /* Ẩn thanh cuộn trên Chrome, Edge và Safari */
    }
}
</style>
