<template>
    <aside class="space-y-4">
        <ProfileIntro :userProfile="profileStore.profile" />
        <ProfilePhotos :photos="profileStore.profile.photos" />
        <ProfileFriends :friends="profileStore.profile.friends" />
    </aside>
</template>

<script setup>
import { useProfileStore } from '@/stores/profileStore'
import ProfileIntro from './ProfileIntro.vue'
import ProfilePhotos from './ProfilePhotos.vue'
import ProfileFriends from './ProfileFriends.vue'

const profileStore = useProfileStore()

console.log(1)
</script>
