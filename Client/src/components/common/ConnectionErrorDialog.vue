<script setup>
import Dialog from 'primevue/dialog'
import Button from 'primevue/button'

const { visible } = defineProps({
    visible: {
        type: Boolean,
        required: true,
    },
})

const emit = defineEmits(['update:visible'])

const reloadPage = () => {
    window.location.reload()
}
</script>

<template>
    <Dialog
        :visible="visible"
        @update:visible="(val) => emit('update:visible', val)"
        :closable="false"
        modal
        :draggable="false"
        header="Connection Error"
    >
        <div class="p-4">
            <p class="mb-4 text-center">
                Lost connection to server. Please reload the page to continue.
            </p>
            <div class="flex justify-center">
                <Button label="Reload Page" @click="reloadPage" severity="primary" />
            </div>
        </div>
    </Dialog>
</template>
