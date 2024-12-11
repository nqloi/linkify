<template>
    <div class="card-wrapper bg-surface-card shadow-sm rounded-lg p-4" :class="customClass">
        <slot />
    </div>
</template>
<script setup>
defineProps({
    customClass: '',
})
</script>
<style lang="scss" scoped>
.card-wrapper {
    background-color: var(--surface-card);
    transition: all var(--element-transition-duration);
}
</style>
