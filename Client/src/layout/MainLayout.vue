<script setup>
import { RouterView } from 'vue-router'
import TheHeader from './TheHeader.vue'
import NavigationSidebar from '@/layout/NavigationSidebar.vue';
</script>

<template>
  <div class="min-h-screen h-screen flex flex-col">
    <TheHeader />
    <div class="p-4 bg-slate-100 h-100 flex flex-column flex-1">
      <div class="flex h-full flex-1 gap-x-3">
        <NavigationSidebar />
        <RouterView />
      </div>
    </div>
  </div>
</template>

<style lang="" scoped>
</style>