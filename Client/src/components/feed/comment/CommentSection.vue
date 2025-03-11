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
            <UserAvatar class="mr-2" :avatarUrl="authStore.user.avatarUrl" />
            <InputText
                v-model="newComment"
                placeholder="Write a comment..."
                class="w-full"
                @keyup.enter="addComment"
            />
            <Button label="Post" class="p-button-sm ml-2" variant="outlined" @click="addComment" />
        </div>
    </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { Avatar, Button, InputText } from 'primevue'
import CommentItem from './CommentItem.vue'
import defaultAvatar from '@/assets/images/avatar-default.svg'
import useCommentService from '@/services/posts/commentService'
import { useAuthStore } from '@/stores/authStore'
import UserAvatar from '@/components/common/UserAvatar.vue'

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
const visibleCommentCount = ref(5) // Number of comments to show initially
const commentsPerLoad = 10 // Number of comments to load each time

// Service
const commentService = useCommentService(props.postId)

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
                    id: authStore.user.userId,
                    avatarUrl: authStore.user.avatarUrl,
                    displayName: authStore.getDisplayName(),
                },
            })
            newComment.value = ''
        }
    }
}

const handleDelete = async (commentId) => {
    try {
        await commentService.delete(commentId)
        comments.value = comments.value.filter((c) => c.id !== commentId)
    } catch (error) {
        console.log(error)
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
