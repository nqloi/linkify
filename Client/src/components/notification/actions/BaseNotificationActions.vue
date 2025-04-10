<template>
    <div class="notification-actions">
        <slot></slot>
    </div>
</template>

<script setup>
defineProps({
    notification: {
        type: Object,
        required: true,
    },
})

defineEmits(['action-complete'])
</script>

<style scoped>
.notification-actions {
    @apply flex gap-2 mt-2;
}
</style>
