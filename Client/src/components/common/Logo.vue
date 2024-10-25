<script setup>
const props = defineProps({
  isDesktop: {
    type: Boolean,
    default: true
  }
})
</script>

<template>
  <img v-if="isDesktop" src="../../assets/images/logo-full-text.png" class="logo" />
  <img v-else src="../../assets/images/logo-small.png" class="logo" />
</template>

<style>
.logo {
  width: 140px;
  height: 38px;
}
</style>