<script setup>
const props = defineProps({
  isDestop: {
    type: Boolean,
    default: true
  }
})
</script>

<template>
  <img src="../../assets/images/logo-v2.png" class="logo" />
</template>

<style>
.logo {
  width: 140px;
  height: 38px;
}
</style>