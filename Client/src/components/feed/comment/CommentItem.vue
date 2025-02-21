<template>
    <div class="comment-item flex gap-2">
        <!-- Avatar -->
        <Avatar
            :image="creator.avatarUrl || defaultAvatar"
            class="w-8 h-8 shrink-0"
            shape="circle"
        />

        <div class="comment-content">
            <!-- User Info -->
            <div class="comment-box bg-gray-100 px-3 py-2 rounded-xl">
                <div class="user-info">
                    <span class="font-bold text-sm">{{ creator.displayName }}</span>
                    <span class="text-xs text-gray-500 ml-2">{{ timeAgo(createdAt) }}</span>
                </div>

                <!-- Comment text -->
                <p class="comment-text" :class="{ truncated: !showFullContent }">
                    {{ showFullContent ? content : truncatedContent }}
                </p>

                <!-- "See more" / "See less" toggle -->
                <span v-if="content?.length > maxLength" @click="toggleExpand" class="see-more">
                    {{ showFullContent ? 'See less' : 'See more' }}
                </span>
            </div>

            <!-- Actions: Like | Reply | Delete (only for owner) -->
            <div class="comment-actions flex gap-3 text-xs text-gray-500">
                <span @click="handleLike" class="cursor-pointer">Like</span>
                <span @click="handleReply" class="cursor-pointer">Reply</span>
                <span v-if="isCommentOwner" @click="handleDelete" class="cursor-pointer"
                    >Delete</span
                >
            </div>

            <!-- Reply input field -->
            <div v-if="showReplyForm" class="reply-form">
                <InputText
                    v-model="replyContent"
                    placeholder="Write a reply..."
                    class="w-full mt-2"
                />
                <Button label="Post Reply" class="p-button-sm mt-1" @click="postReply" />
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, computed } from 'vue'
import { useAuthStore } from '@/stores/authStore'
import { timeAgo } from '@/utils/time/timeUtil'
import defaultAvatar from '@/assets/images/avatar-default.svg'
import { Avatar, InputText, Button } from 'primevue'

// Props - Receives comment data from parent
const props = defineProps({
    id: {
        type: String,
        required: true,
    },
    content: {
        type: String,
    },
    creator: {
        type: Object,
        default: () => ({
            id: null,
            displayName: '',
            avatarUrl: '',
        }),
    },
    createdAt: {
        type: String,
        required: true,
    },
})

// Emits - For parent component communication
const emit = defineEmits(['like', 'reply', 'delete'])

// Character limit for collapsed comments
const maxLength = 120
const showFullContent = ref(false)

const truncatedContent = computed(() => {
    return props.content?.length > maxLength
        ? props.content.slice(0, maxLength) + '...'
        : props.content
})

const toggleExpand = () => {
    showFullContent.value = !showFullContent.value
}

// Check if the current user is the comment owner
const authStore = useAuthStore()
const isCommentOwner = computed(() => authStore.user?.userId === props.creator.id)

// Action handlers
const handleLike = () => emit('like', props.id)
const handleReply = () => (showReplyForm.value = !showReplyForm.value)
const handleDelete = () => emit('delete', props.id)

// Reply input state
const showReplyForm = ref(false)
const replyContent = ref('')

const postReply = () => {
    if (replyContent.value.trim()) {
        emit('reply', { commentId: props.id, content: replyContent.value })
        replyContent.value = ''
        showReplyForm.value = false
    }
}
</script>

<style scoped>
/* Ensure text wraps properly */
.comment-text {
    word-wrap: break-word;
    white-space: pre-wrap;
    overflow-wrap: break-word;
    max-width: 100%;
    display: block;
}

/* Add a smooth transition when expanding */
.comment-box {
    max-width: 100%;
    word-break: break-word;
}

/* Style for "See more" / "See less" */
.see-more {
    color: blue;
    cursor: pointer;
    font-size: 0.85rem;
    margin-left: 5px;
}

/* Fix comment box alignment */
.comment-item {
    display: flex;
    align-items: flex-start;
    gap: 10px;
}
</style>
