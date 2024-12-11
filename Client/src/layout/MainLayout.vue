<script setup>
import { RouterView } from 'vue-router'
import TheHeader from './TheHeader.vue'
import NavigationSidebar from '@/layout/NavigationSidebar.vue'
import SidebarRight from './SidebarRight.vue'
</script>

<template>
    <div class="h-screen flex flex-col">
        <TheHeader />
        <div class="p-4 bg-slate-100 flex flex-column gap-4 flex-1 overflow-hidden">
            <NavigationSidebar />
            <RouterView />
            <SidebarRight />
        </div>
    </div>
</template>

<style lang="" scoped></style>
