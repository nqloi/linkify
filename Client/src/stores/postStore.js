import usePostService from '@/services/postService'
import { defineStore } from 'pinia'
import { ref } from 'vue'

export const usePostStore = defineStore('post', () => {
    const postService = usePostService()

    // State
    const posts = ref([])

    // Actions
    const addPost = async (newPost) => {
        const response = await postService.create(newPost)
        response.content && posts.value.unshift(newPost)
    }

    const setPosts = (posts) => {
        posts.value = posts
    }

    const initPosts = async () => {
        const response = await postService.getAll()
        posts.value = response?.content ?? []
    }

    return { posts, addPost, setPosts, initPosts }
})
