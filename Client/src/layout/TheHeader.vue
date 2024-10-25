<template>
  <div class="flex py-2 px-4 items-center justify-between shadow-md">
    <Logo />
    <IconField>
      <InputIcon class="pi pi-search" />
      <InputText v-model="searchText" placeholder="Search" />
    </IconField>
    <div class="right-icons">
      <div :onclick="toggleDarkMode">Toggle Mode</div>
    </div>
  </div>
</template>
<script setup>
import Logo from '@/components/common/Logo.vue'
import IconField from 'primevue/iconfield'
import InputIcon from 'primevue/inputicon'

import { ref } from 'vue'
const searchText = ref('')

const toggleDarkMode = () => {
  document.documentElement.classList.toggle('dark');
};
</script>
<style lang="" scoped>
</style>