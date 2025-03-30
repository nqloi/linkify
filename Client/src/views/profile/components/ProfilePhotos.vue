<template>
    <Card>
        <template #title>
            <div class="flex justify-between items-center">
                <span class="text-xl font-bold">Photos</span>
                <Button v-if="photos.length" label="See All" link @click="$emit('see-all')" />
            </div>
        </template>
        <template #content>
            <div v-if="loading" class="flex justify-center">
                <ProgressSpinner />
            </div>

            <div v-else-if="photos.length" class="grid grid-cols-3 gap-2">
                <div
                    v-for="photo in displayedPhotos"
                    :key="photo.id"
                    class="aspect-square rounded-lg overflow-hidden cursor-pointer hover:opacity-90"
                    @click="$emit('view-photo', photo)"
                >
                    <img
                        :src="photo.url"
                        :alt="photo.description"
                        class="w-full h-full object-cover"
                        @error="handleImageError"
                    />
                </div>
            </div>

            <div v-else class="text-center text-gray-500 py-4">No photos to display</div>
        </template>
    </Card>
</template>

<script setup>
import { computed } from 'vue'
import Card from 'primevue/card'
import Button from 'primevue/button'
import ProgressSpinner from 'primevue/progressspinner'

const props = defineProps({
    photos: {
        type: Array,
        default: () => [],
    },
    loading: {
        type: Boolean,
        default: false,
    },
})

const displayedPhotos = computed(() => {
    return props.photos.slice(0, 9) // Show only first 9 photos
})

const handleImageError = (e) => {
    e.target.src = '/default-photo.jpg'
}
</script>
