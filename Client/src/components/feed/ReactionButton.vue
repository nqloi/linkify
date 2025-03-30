<template>
    <div class="relative flex items-center" @mouseleave="scheduleHideReactions">
        <Button
            class="flex items-center gap-2 font-medium"
            :class="selectedReaction.textColor"
            variant="text"
            @click="toggleReaction"
            @mouseover="scheduleShowReactions"
        >
            <img v-if="!!selectedReaction.icon" :src="selectedReaction.icon" class="w-5 h-5" />
            {{ selectedReaction.label }}
        </Button>
        <Transition name="fade">
            <div
                v-if="showReactions"
                class="absolute bottom-full left-0 flex gap-2 p-3 bg-white shadow-lg rounded-lg"
                @mouseover="cancelHideReactions"
                @mouseleave="scheduleHideReactions"
            >
                <button
                    v-for="reaction in reactions"
                    :key="reaction.type"
                    @click="setReaction(reaction)"
                    class="hover:scale-150 transition-transform w-10"
                >
                    <img
                        :src="reaction.icon"
                        :alt="reaction.label"
                        class="w-8 h-8 object-contain"
                    />
                </button>
            </div>
        </Transition>
    </div>
</template>

<script setup>
import { ref } from 'vue'
import reactionType from '@/common/enums/reactionType'
import useReactionService from '@/services/posts/reactionService'
import { Button } from 'primevue'

const props = defineProps({
    postId: { type: String, required: true },
    userReactionType: { reactionType: Number, default: null },
})

const reactionService = useReactionService(props.postId)
const showReactions = ref(false)
const hideTimeout = ref(null)

const defaultReaction = {
    type: reactionType.None,
    icon: '',
    label: 'Like',
    textColor: 'text-gray-600',
}

const reactions = [
    {
        type: reactionType.Like,
        icon: new URL('@/assets/icons/reactions/like.svg', import.meta.url).href,
        label: 'Like',
        textColor: 'text-primary',
    },
    {
        type: reactionType.Love,
        icon: new URL('@/assets/icons/reactions/love.svg', import.meta.url).href,
        label: 'Love',
        textColor: 'text-red-500',
    },
    {
        type: reactionType.Haha,
        icon: new URL('@/assets/icons/reactions/haha.svg', import.meta.url).href,
        label: 'Haha',
        textColor: 'text-yellow-500',
    },
    {
        type: reactionType.Wow,
        icon: new URL('@/assets/icons/reactions/wow.svg', import.meta.url).href,
        label: 'Wow',
        textColor: 'text-orange-500',
    },
    {
        type: reactionType.Sad,
        icon: new URL('@/assets/icons/reactions/sad.svg', import.meta.url).href,
        label: 'Sad',
        textColor: 'text-gray-600',
    },
    {
        type: reactionType.Angry,
        icon: new URL('@/assets/icons/reactions/angry.svg', import.meta.url).href,
        label: 'Angry',
        textColor: 'text-red-600',
    },
]

const getReactionByType = (reactionType) =>
    reactions.find((r) => r.type === reactionType) || defaultReaction

const selectedReaction = ref(
    props.userReactionType ? getReactionByType(props.userReactionType) : defaultReaction,
)

const setReaction = async (reaction) => {
    selectedReaction.value = reaction
    showReactions.value = false
    await reactionService.addOrUpdate(reaction.type)
}

const toggleReaction = async () => {
    if (selectedReaction.value.type) {
        selectedReaction.value = defaultReaction
        await reactionService.delete()
    } else {
        selectedReaction.value = getReactionByType(reactionType.Like)
        await reactionService.addOrUpdate(reactionType.Like)
    }
}

const scheduleHideReactions = () => {
    hideTimeout.value = setTimeout(() => (showReactions.value = false), 700)
}

const scheduleShowReactions = () => {
    setTimeout(() => (showReactions.value = true), 700)
}

const cancelHideReactions = () => {
    clearTimeout(hideTimeout.value)
}
</script>

<style scoped>
.fade-enter-active,
.fade-leave-active {
    transition: opacity 0.2s;
}
.fade-enter,
.fade-leave-to {
    opacity: 0;
}
</style>
