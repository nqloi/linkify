<template>
    <div class="infinite-scroll-container" ref="container">
        <slot />
        <div class="infinite-scroll-trigger" ref="trigger">
            <div v-if="loading" class="loading-indicator">
                <ProgressSpinner style="width: 30px; height: 30px" />
            </div>
            <div v-if="error" class="error-message">
                {{ error }}
                <Button label="Retry" @click="$emit('retry')" class="p-button-sm" />
            </div>
            <div v-if="!hasMore && !loading" class="end-message">
                {{ endMessage }}
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, onMounted, onUnmounted } from 'vue'
import ProgressSpinner from 'primevue/progressspinner'

const props = defineProps({
    loading: {
        type: Boolean,
        default: false,
    },
    error: {
        type: String,
        default: '',
    },
    hasMore: {
        type: Boolean,
        default: true,
    },
    threshold: {
        type: Number,
        default: 100,
    },
    endMessage: {
        type: String,
        default: 'No more items to load',
    },
    disabled: {
        type: Boolean,
        default: false,
    },
})

const emit = defineEmits(['load-more', 'retry'])

const container = ref(null)
const trigger = ref(null)
let observer = null

const setupIntersectionObserver = () => {
    observer = new IntersectionObserver(
        (entries) => {
            const triggerElement = entries[0]
            if (
                triggerElement.isIntersecting &&
                !props.loading &&
                !props.error &&
                props.hasMore &&
                !props.disabled
            ) {
                emit('load-more')
            }
        },
        {
            root: null,
            rootMargin: `0px 0px ${props.threshold}px 0px`,
            threshold: 0,
        },
    )

    if (trigger.value) {
        observer.observe(trigger.value)
    }
}

onMounted(() => {
    setupIntersectionObserver()
})

onUnmounted(() => {
    if (observer) {
        observer.disconnect()
    }
})
</script>

<style scoped>
.infinite-scroll-container {
    position: relative;
    min-height: 100px;
}

.infinite-scroll-trigger {
    width: 100%;
    height: 50px;
    display: flex;
    justify-content: center;
    align-items: center;
}

.loading-indicator,
.error-message,
.end-message {
    padding: 1rem;
    text-align: center;
    color: var(--text-color-secondary);
}

.error-message {
    color: var(--red-500);
}
</style>
