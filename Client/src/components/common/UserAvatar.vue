<template>
    <Avatar
        :image="props.avatarUrl ?? defaultAvatar"
        alt="User Avatar"
        :class="avatarSizeClasses"
        class="rounded-full cursor-pointer"
        :style="avatarStyle"
        @click="goToProfile"
    />
</template>

<script setup>
import defaultAvatar from '@/assets/images/avatar-default.svg'
import { computed } from 'vue'
import { SIZE, TAILWIND_SIZE_MAP } from '@/common/constants/size'
import { Avatar } from 'primevue'
import { useRouter } from 'vue-router'

const props = defineProps({
    avatarUrl: {
        type: String,
    },
    size: {
        type: String,
        default: SIZE.MD, // Use the constant for the default
        validator: (value) => Object.values(SIZE).includes(value), // Validate using constant values
    },
    customSize: {
        type: String,
        default: null,
    },
    userId: {
        type: String,
        default: null,
        required: true,
    },
})

const router = useRouter()

const avatarSizeClasses = computed(() => {
    return TAILWIND_SIZE_MAP[props.size] || TAILWIND_SIZE_MAP[SIZE.MD] // Use the mapping or default to medium
})

const avatarStyle = computed(() => {
    if (props.customSize) {
        return {
            width: props.customSize,
            height: props.customSize,
        }
    }
    return null
})

const goToProfile = () => {
    router.push({ name: 'Profile', params: { id: props.userId } })
}
</script>

<style lang="scss" scoped></style>
