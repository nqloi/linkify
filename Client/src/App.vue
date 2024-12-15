<script setup>
import ConfirmDialog from 'primevue/confirmdialog'
import SessionTimeoutDialog from './components/authen/dialog/SessionTimeoutDialog.vue'
import Toast from 'primevue/toast'
import GlobalLoading from './components/common/GlobalLoading.vue'
</script>

<template>
    <ConfirmDialog></ConfirmDialog>
    <SessionTimeoutDialog />
    <Toast></Toast>
    <router-view />
    <GlobalLoading> </GlobalLoading>
</template>

<style scoped></style>
