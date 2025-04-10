<script setup>
import { onMounted } from 'vue'
import ConfirmDialog from 'primevue/confirmdialog'
import SessionTimeoutDialog from './components/authen/dialog/SessionTimeoutDialog.vue'
import Toast from 'primevue/toast'
import GlobalLoading from './components/common/GlobalLoading.vue'
import ConnectionErrorDialog from './components/common/ConnectionErrorDialog.vue'
import { useNotificationStore } from './stores/notificationStore'
import useNotificationHub from './composables/useNotificationHub'
import { logger } from './utils/logger'

const notificationStore = useNotificationStore()
const { showReloadDialog } = useNotificationHub()

onMounted(async () => {
    try {
        await notificationStore.initializeNotificationService()
    } catch (error) {
        logger.error('Failed to initialize notification service:', error)
    }
})
</script>

<template>
    <ConfirmDialog></ConfirmDialog>
    <SessionTimeoutDialog />
    <Toast></Toast>
    <router-view />
    <GlobalLoading />
    <ConnectionErrorDialog v-model:visible="showReloadDialog" />
</template>

<style scoped></style>
