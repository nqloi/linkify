<template>
    <div v-if="visible" class="global-loading-overlay">
        <div class="flex flex-col items-center justify-center h-full">
            <ProgressSpinner :style="{ width: '50px', height: '50px' }" class="text-primary" />
            <p v-if="message" class="mt-4 text-white font-medium text-center">{{ message }}</p>
        </div>
    </div>
</template>

<script setup>
import { computed } from 'vue'
import ProgressSpinner from 'primevue/progressspinner'
import { useLoadingStore } from '@/stores/loadingStore'

const loadingStore = useLoadingStore()
const visible = computed(() => loadingStore.visible)
const message = computed(() => loadingStore.message)
</script>

<style scoped>
.global-loading-overlay {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    z-index: 1200; /* Đảm bảo cao hơn Dialog */
    background-color: rgba(0, 0, 0, 0.3); /* Overlay màu đen mờ */
    display: flex;
    align-items: center;
    justify-content: center;
}
</style>
