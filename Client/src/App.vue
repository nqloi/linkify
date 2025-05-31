<template>
    <ConfirmDialog></ConfirmDialog>
    <SessionTimeoutDialog />
    <Toast position="bottom-right"></Toast>

    <!-- Notification toasts -->
    <NotificationToast />
    <router-view />
    <GlobalLoading />
    <ConnectionErrorDialog v-model:visible="showReloadDialog" />
</template>

<script setup>
import ConfirmDialog from 'primevue/confirmdialog'
import Toast from 'primevue/toast'
import SessionTimeoutDialog from './components/authen/dialog/SessionTimeoutDialog.vue'
import ConnectionErrorDialog from './components/common/ConnectionErrorDialog.vue'
import GlobalLoading from './components/common/GlobalLoading.vue'
import NotificationToast from './components/notification/NotificationToast.vue'
import useNotificationHub from './composables/useNotificationHub'
import { APP_TITLE } from './utils/envConfig'

// Set document title from environment variable
document.title = APP_TITLE

const { showReloadDialog } = useNotificationHub()
</script>

<style scoped></style>
