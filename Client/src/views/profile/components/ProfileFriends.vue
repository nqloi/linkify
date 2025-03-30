<template>
    <Card>
        <template #title>
            <div class="flex justify-between items-center">
                <div>
                    <span class="text-xl font-bold">Friends</span>
                    <span class="text-gray-600 text-sm ml-2">
                        {{ friends.length }}
                    </span>
                </div>
                <Button v-if="friends.length" label="See All" link @click="$emit('see-all')" />
            </div>
        </template>
        <template #content>
            <div v-if="loading" class="flex justify-center">
                <ProgressSpinner />
            </div>

            <div v-else-if="friends.length" class="grid grid-cols-3 gap-3">
                <div
                    v-for="friend in displayedFriends"
                    :key="friend.id"
                    class="friend-card cursor-pointer"
                    @click="$emit('view-profile', friend.id)"
                >
                    <div class="aspect-square rounded-lg overflow-hidden mb-2">
                        <img
                            :src="friend.avatarUrl"
                            :alt="friend.displayName"
                            class="w-full h-full object-cover"
                            @error="handleImageError"
                        />
                    </div>
                    <h3 class="text-sm font-semibold truncate">
                        {{ friend.displayName }}
                    </h3>
                    <p v-if="friend.mutualFriends" class="text-xs text-gray-500">
                        {{ friend.mutualFriends }} mutual friends
                    </p>
                </div>
            </div>

            <div v-else class="text-center text-gray-500 py-4">No friends to display</div>
        </template>
    </Card>
</template>

<script setup>
import { computed } from 'vue'
import Card from 'primevue/card'
import Button from 'primevue/button'
import ProgressSpinner from 'primevue/progressspinner'

const props = defineProps({
    friends: {
        type: Array,
        default: () => [],
    },
    loading: {
        type: Boolean,
        default: false,
    },
})

const displayedFriends = computed(() => {
    return props.friends.slice(0, 9) // Show only first 9 friends
})

const handleImageError = (e) => {
    e.target.src = '/default-avatar.jpg'
}
</script>

<style scoped>
.friend-card:hover img {
    transform: scale(1.05);
    transition: transform 0.2s ease;
}
</style>
