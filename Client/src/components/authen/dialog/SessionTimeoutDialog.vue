<template>
    <Dialog
        v-model:visible="visible"
        header="Session Expired"
        modal
        :closable="false"
        :style="{ width: '30vw' }"
    >
        <div class="p-d-flex p-ai-center p-jc-center">
            <p>Your session has expired. Please log in again to continue.</p>
        </div>
        <template #footer>
            <Button label="Log in" @click="handleLoginAgain" />
        </template>
    </Dialog>
</template>

<script setup>
import router from '@/router'
import { useAuthStore } from '@/stores/authStore'
import { storeToRefs } from 'pinia'
import { ref, watch } from 'vue'
const visible = ref(false)

const { isSessionTimeout } = storeToRefs(useAuthStore())

watch(
    isSessionTimeout,
    () => {
        visible.value = isSessionTimeout.value
    },
    {
        deep: true,
    },
)

const handleLoginAgain = async () => {
    visible.value = false
    router.push('/auth/login')
}
</script>

<style lang="scss" scoped></style>
