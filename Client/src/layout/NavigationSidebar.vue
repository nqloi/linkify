<template>
  <div class="h-full w-60 flex flex-col"> <!-- Sidebar -->
    <div class="card dark-card">
      <div class="flex items-center">
        <i class="pi pi-user" style="font-size: 2.5rem"></i>
        <div class="ml-4">
          <div>{{ user?.firstName ?? '' }} {{ user?.lastName ?? '' }}</div>
          <div>
            @{{ user?.userName ?? '' }}
          </div>
        </div>
      </div>
    </div>
    <div class="card dark-card">second</div>
    <div class="card dark-card flex-1">third</div>
  </div>
</template>
<script setup>
import { useAuthStore } from '@/stores/authStore';

const { user } = useAuthStore()
</script>

<style scoped>
.dark-card {
  @apply dark:bg-surface-0 dark:text-white
}
</style>