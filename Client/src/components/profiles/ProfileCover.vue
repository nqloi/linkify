<template>
    <div class="profile-cover relative group">
        <!-- Cover Container -->
        <div class="cover-container relative h-[350px] bg-gradient-to-r from-gray-100 to-gray-200">
            <!-- Cover Image -->
            <img
                v-if="coverUrl"
                :src="coverUrl"
                class="w-full h-full object-cover transition-transform duration-300 group-hover:scale-[1.02]"
                alt="Cover photo"
                @error="handleImageError"
            />

            <!-- No Cover Placeholder -->
            <div v-else class="flex items-center justify-center h-full text-gray-400">
                <div class="text-center">
                    <i class="pi pi-image text-4xl mb-2"></i>
                    <p class="text-sm">No cover photo added yet</p>
                </div>
            </div>

            <!-- Overlay on Hover -->
            <div
                class="absolute inset-0 bg-black/30 opacity-0 group-hover:opacity-100 transition-opacity duration-300 flex items-center justify-center cursor-pointer"
                @click="handleViewCover"
            ></div>

            <!-- Loading Overlay -->
            <div
                v-if="loading"
                class="absolute inset-0 bg-black/50 flex items-center justify-center z-50"
            >
                <ProgressSpinner strokeWidth="4" />
            </div>
        </div>

        <!-- Quick Actions Menu (Only for current user) -->
        <div v-if="isCurrentUser" class="absolute bottom-4 right-4 flex">
            <Button
                icon="pi pi-camera"
                @click="$emit('change-cover')"
                class="min-w-44"
                severity="secondary"
                >Edit your cover photo</Button
            >
        </div>
    </div>
</template>

<script setup>
import { useImageViewerStore } from '@/stores/imageViewerStore'
import ProgressSpinner from 'primevue/progressspinner'

const props = defineProps({
    coverUrl: {
        type: String,
        default: '',
    },
    isCurrentUser: {
        type: Boolean,
        default: false,
    },
    loading: {
        type: Boolean,
        default: false,
    },
})
// Store
const imageViewerStore = useImageViewerStore()

const emit = defineEmits(['change-cover', 'remove-cover'])

const handleImageError = (e) => {
    e.target.remove() // Remove broken image and fallback to gradient background
}

const handleViewCover = () => {
    if (props.coverUrl) {
        imageViewerStore.openViewer(props.coverUrl)
    }
}
</script>

<style scoped>
.profile-cover {
    width: 100%;
    z-index: 10; /* Add this */
}

.cover-container {
    position: relative;
    overflow: hidden;
    background-size: 200% 200%;
    animation: gradient 15s ease infinite;
}

@keyframes gradient {
    0% {
        background-position: 0% 50%;
    }
    50% {
        background-position: 100% 50%;
    }
    100% {
        background-position: 0% 50%;
    }
}

/* Smooth transitions */
.transition-transform {
    transition-property: transform;
    transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
    transition-duration: 300ms;
}

.transition-opacity {
    transition-property: opacity;
    transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
    transition-duration: 300ms;
}

/* Add these new styles */
:deep(.p-button) {
    position: relative;
    z-index: 60;
}

:deep(.p-button:hover) {
    transform: translateY(-1px);
    transition: all 0.2s ease;
}
</style>
