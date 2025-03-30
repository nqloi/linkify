<script setup>
import { useRouter } from 'vue-router'

const props = defineProps({
    isDesktop: {
        type: Boolean,
        default: true,
    },
})

const router = useRouter()
</script>

<template>
    <img
        v-if="isDesktop"
        src="../../assets/images/logo-full-text.png"
        class="logo cursor-pointer"
        @click="router.push('/')"
    />
    <img v-else src="../../assets/images/logo-small.png" class="logo" @click="router.push('/')" />
</template>

<style>
.logo {
    width: 140px;
    height: 38px;
}
</style>
