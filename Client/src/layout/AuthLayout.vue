<!-- src/layouts/AuthLayout.vue -->
<template>
    <div class="flex flex-col bg-neutral-100 py-4 px-4 h-screen">
        <Logo class="mb-4"></Logo>
        <div class="flex justify-center items-center flex-1">
            <router-view />
        </div>
    </div>
</template>

<script setup>
import Logo from '@/components/common/Logo.vue'
</script>

<style scoped>
/* Thêm CSS tùy chỉnh nếu cần */
</style>
