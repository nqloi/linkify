<template>
    <div class=" h-screen w-screen justify-center items-center flex-col flex">
        <h1>404 - Page Not Found</h1>
        <p>Sorry, the page you are looking for does not exist.</p>
        <router-link to="/" class="text-primary-500 font-semibold">Go back to Home</router-link>
    </div>
</template>

<script setup>
import { RouterLink } from 'vue-router';
</script>

<style scoped></style>