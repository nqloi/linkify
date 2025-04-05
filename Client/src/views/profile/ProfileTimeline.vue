<template>
    <div class="grid grid-cols-1 md:grid-cols-3 gap-4">
        <!-- Left Sidebar -->
        <ProfileSidebar class="md:col-span-1" />

        <!-- Main Content -->
        <div class="md:col-span-2">
            <PostInput class="mb-4" />
            <ProfilePosts />
        </div>
    </div>
</template>

<script setup>
import PostInput from '@/components/feed/PostInput.vue'
import ProfilePosts from './components/ProfilePosts.vue'
import ProfileSidebar from './components/ProfileSidebar.vue'
</script>
