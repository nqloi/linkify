<template>
    <div class="h-screen flex flex-col">
        <TheHeader />

        <div class="flex-1 bg-slate-100 overflow-y-auto">
            <!-- Fixed Profile Header Section -->
            <ProfileHeader />

            <!-- Main Content Area -->
            <main class="max-w-7xl mx-auto px-4 py-4">
                <RouterView />
            </main>
        </div>
    </div>
</template>

<script setup>
import { RouterView } from 'vue-router'
import TheHeader from './TheHeader.vue'
import ProfileHeader from '@/components/profiles/ProfileHeader.vue'
import { onMounted } from 'vue'
import { useProfileStore } from '@/stores/profileStore'
const profileStore = useProfileStore()

onMounted(() => {
    profileStore.loadProfile()
})
</script>
