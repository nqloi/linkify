<template>
    <div class="grid grid-cols-1 md:grid-cols-3 gap-4">
        <!-- Left Sidebar -->
        <ProfileSidebar class="md:col-span-1" />

        <!-- Main Content -->
        <div class="md:col-span-2">
            <!-- <CreatePost
                v-if="profileStore.isCurrentUser"
                @create="profileStore.handleCreatePost"
                class="mb-4"
            /> -->
            <ProfilePosts />
        </div>
    </div>
</template>

<script setup>
import { onMounted } from 'vue'
import { useProfileStore } from '@/stores/profileStore'
import ProfileSidebar from './components/ProfileSidebar.vue'
import ProfilePosts from './components/ProfilePosts.vue'
// import CreatePost from '@/components/common/CreatePost.vue'
</script>
