<script setup>
import ConfirmDialog from 'primevue/confirmdialog'
import SessionTimeoutDialog from './components/authen/dialog/SessionTimeoutDialog.vue'
import Toast from 'primevue/toast'
</script>

<template>
    <ConfirmDialog></ConfirmDialog>
    <SessionTimeoutDialog />
    <Toast></Toast>
    <router-view />
</template>

<style scoped></style>
