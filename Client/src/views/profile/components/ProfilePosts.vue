<template>
    <div class="profile-posts">
        <div v-if="loading && !posts.length" class="flex justify-center p-4">
            <ProgressSpinner />
        </div>

        <div v-else-if="posts.length === 0" class="text-center py-8">
            <i class="pi pi-inbox text-4xl text-gray-400 mb-2"></i>
            <p class="text-gray-500">No posts to show</p>
        </div>

        <InfiniteScroll
            v-else
            :loading="loading"
            :error="error"
            :has-more="hasMorePosts"
            @load-more="loadMorePosts"
            @retry="loadMorePosts"
        >
            <div class="space-y-4">
                <FeedPost
                    v-for="post in posts"
                    :key="post.id"
                    :id="post.id"
                    :content="post.content"
                    :imageUrls="post.imageUrls"
                    :creator="post.creator"
                    :createdAt="post.createdAt"
                    :stats="post.stats"
                    :userActions="post.userActions"
                />
            </div>
        </InfiniteScroll>

        <ImageViewer />
    </div>
</template>

<script setup>
import { ref, onUnmounted, onMounted } from 'vue'
import FeedPost from '@/components/feed/FeedPost.vue'
import ImageViewer from '@/components/common/ImageViewer.vue'
import InfiniteScroll from '@/components/common/InfiniteScroll.vue'
import ProgressSpinner from 'primevue/progressspinner'
import { useToast } from 'primevue/usetoast'
import { useRoute } from 'vue-router'
import usePostService from '@/services/posts/postService'

const toast = useToast()
const postService = usePostService()
const route = useRoute()

const loading = ref(false)
const error = ref('')
const cursor = ref('')
const hasMorePosts = ref(true)
const posts = ref([])

// Methods
const loadMorePosts = async () => {
    try {
        loading.value = true
        error.value = ''

        const response = await postService.getCursorPagedUserPosts(route.params.id, {
            cursor: cursor.value,
        })

        const { content } = response ?? {}
        posts.value = [...posts.value, ...(content.items ?? [])]
        cursor.value = content.cursor
        hasMorePosts.value = content.hasNextPage
    } catch (err) {
        error.value = 'Failed to load posts'
        console.error(err)
        toast.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Failed to load more posts',
        })
    } finally {
        loading.value = false
    }
}

const initializePosts = async () => {
    try {
        loading.value = true
        error.value = ''

        const response = await postService.getCursorPagedUserPosts(route.params.id)
        const { content } = response ?? {}

        posts.value = content.items ?? []
        cursor.value = content.cursor
        hasMorePosts.value = content.hasNextPage
    } catch (err) {
        error.value = 'Failed to load posts'
        console.error(err)
        toast.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Failed to load posts',
        })
    } finally {
        loading.value = false
    }
}

onMounted(() => {
    initializePosts()
})

onUnmounted(() => {
    cursor.value = null
    hasMorePosts.value = true
    posts.value = []
})
</script>
