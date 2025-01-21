import usePostService from '@/services/postService'
import { defineStore } from 'pinia'
import { ref } from 'vue'
import { useAuthStore } from './authStore'

export const usePostStore = defineStore('post', () => {
    const postService = usePostService()
    const { user, getDisplayName } = useAuthStore()
    // State
    const posts = ref([])

    // Actions
    const addPost = async (newPost) => {
        const response = await postService.create(newPost)
        const result = response.content
        result.creator = {
            id: user.userId,
            displayName: getDisplayName(),
            avatarUrl: user.avatarUrl,
        }

        posts.value.unshift(result)
    }

    const setPosts = (posts) => {
        posts.value = posts
    }

    const initPosts = async () => {
        const response = await postService.getAll()
        posts.value = response?.content ?? []
    }

    const deletePost = async (postId) => {
        await postService.delete(postId)
        posts.value = posts.value.filter((item) => item.id !== postId)
    }

    return { posts, addPost, setPosts, initPosts, deletePost }
})
