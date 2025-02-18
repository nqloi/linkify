<template>
    <div>
        <!-- Display list of comments -->
        <div class="pb-2">
            <CommentItem
                v-for="comment in comments"
                :key="comment.id"
                :id="comment.id"
                :content="comment.content"
                :creator="comment.creator"
                :createdAt="comment.createdAt"
                class="mt-2"
                @like="handleLike"
                @reply="handleReply"
                @delete="handleDelete"
            />
        </div>

        <!-- Show more comments button -->
        <div v-if="hasMoreComments" class="mt-2">
            <Button
                label="Show more comments"
                class="p-button-text p-button-sm"
                @click="loadMoreComments"
            />
        </div>

        <!-- Add new comment form -->
        <div class="pt-2 flex shadow-top">
            <Avatar
                :image="authStore.user.avatarUrl || defaultAvatar"
                class="w-8 h-8 shrink-0 mr-2"
                shape="circle"
            />
            <InputText v-model="newComment" placeholder="Write a comment..." class="w-full" />
            <Button label="Post" class="p-button-sm ml-2" variant="outlined" @click="addComment" />
        </div>
    </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { Avatar, Button, InputText } from 'primevue'
import CommentItem from './CommentItem.vue'
import defaultAvatar from '@/assets/images/avatar-default.svg'
import { timeAgo } from '@/utils/time/timeUtil'
import useCommentService from '@/services/commentService'
import { useAuthStore } from '@/stores/authStore'

// Props for the component
const props = defineProps({
    postId: {
        type: String,
        required: true,
    },
})
// store
const authStore = useAuthStore()

// Reactive state for comments and new comment input
const comments = ref([])
const newComment = ref('')
const visibleCommentCount = ref(2) // Number of comments to show initially
const commentsPerLoad = 10 // Number of comments to load each time

// Service
const commentService = useCommentService(props.postId)

// Computed property to get visible comments
const visibleComments = computed(() => comments.value.slice(0, visibleCommentCount.value))

// Computed property to check if there are more comments to load
const hasMoreComments = computed(() => visibleCommentCount.value < comments.value.length)

// Function to load more comments
const loadMoreComments = () => {
    visibleCommentCount.value += commentsPerLoad
}

// Function to add a new comment
const addComment = async () => {
    if (newComment.value.trim()) {
        // Call API to add a new comment
        const response = await commentService.create({
            postId: props.postId,
            content: newComment.value.trim(),
        })

        if (response.content) {
            comments.value.push({
                id: response.content,
                content: newComment.value,
                creator: {
                    userId: authStore.user.userId,
                    avatarUrl: authStore.user.avatarUrl,
                    displayName: authStore.getDisplayName(),
                },
            })
            newComment.value = ''
        }
    }
}

onMounted(async () => {
    const commentService = useCommentService(props.postId)
    const response = await commentService.getAll()
    comments.value.push(...response.content)
})
</script>

<style lang="scss" scoped>
.shadow-top {
    @apply relative before:content-[''] before:absolute before:bottom-full before:left-0 before:w-full 
               before:h-[4px] before:bg-gradient-to-t before:from-black/5 before:to-transparent;
}
</style>
