<template>
    <div class="feed-post bg-white pt-4 rounded-lg shadow-md">
        <!-- Header -->
        <div class="post-header flex items-center justify-between mb-4 mx-4">
            <div class="flex items-center">
                <UserAvatar class="mr-3" :user="creator" />
                <div>
                    <h5 class="font-bold">{{ props.creator.displayName }}</h5>
                    <p class="text-gray-500 text-xs">{{ timeAgo(props.createdAt) }}</p>
                </div>
            </div>
            <Button icon="pi pi-ellipsis-h" class="p-button-text" @click="toggleMenuAction" />
            <Menu ref="actionMenu" id="action-menu" :model="actionItems" :popup="true" />
        </div>

        <!-- Content -->
        <div class="post-content mb-4 mx-4">
            <p class="0 mb-3">{{ content }}</p>

            <!-- Image Grid -->
            <div v-if="imageUrls?.length" class="grid gap-2 bg-gray-100" :class="gridColsClass">
                <template v-for="(url, index) in visibleImages" :key="index">
                    <div
                        class="relative overflow-hidden rounded-lg flex items-center justify-center bg-gray-300 cursor-pointer"
                        :class="imageWrapperClass(index)"
                        @click="openModal(index)"
                    >
                        <Image
                            :src="url"
                            alt="Post Image"
                            :imageClass="`w-auto h-auto ${imageHeightClass} object-contain`"
                        />
                        <template v-if="index === 3 && imageUrls.length > 4">
                            <div
                                class="absolute inset-0 bg-black bg-opacity-50 text-white flex items-center justify-center text-2xl font-bold"
                            >
                                +{{ imageUrls.length - 4 }}
                            </div>
                        </template>
                    </div>
                </template>
            </div>
        </div>

        <!-- Footer (Reactions) -->
        <div
            class="flex items-center justify-between text-sm text-gray-600 bg-gray-50 dark:bg-gray-800 shadow-sm rounded-b-lg px-4"
        >
            <div class="flex items-center gap-2 sm:gap-4">
                <!-- Like Button -->
                <ReactionButton :postId="id" :userReactionType="userActions?.reactionType" />
                <!-- Comment Button -->
                <Button
                    icon="pi pi-comment"
                    label="Comment"
                    class="p-button-text p-button flex items-center gap-1 text-gray-600 hover:text-primary transition"
                    @click="handleShowComment"
                />
                <!-- Share Button -->
                <Button
                    icon="pi pi-share-alt"
                    label="Share"
                    class="p-button-text p-button flex items-center gap-1 text-gray-600 hover:text-primary transition"
                />
            </div>

            <!-- Reaction Summary -->
            <p class="text-xs sm:text-sm text-gray-500">
                <span class="font-semibold text-gray-700 dark:text-gray-300">{{
                    reactions?.likes
                }}</span>
                {{ stats?.reactionCount ?? 0 }} Like •
                <span class="font-semibold text-gray-700 dark:text-gray-300">{{
                    reactions?.comments
                }}</span>
                {{ stats?.commentCount ?? 0 }} Comment •
                <span class="font-semibold text-gray-700 dark:text-gray-300">{{
                    reactions?.shares
                }}</span>
                Share
            </p>
        </div>

        <!-- Comment Section -->
        <CommentSection :postId="id" v-if="showComments" class="p-4" />
    </div>
</template>

<script setup>
import { useLoadingStore } from '@/stores/loadingStore'
import { usePostStore } from '@/stores/postStore'
import { timeAgo } from '@/utils/timeUtil'
import { useCustomToast } from '@/utils/toast/customToast'
import { Image, Menu } from 'primevue'
import { computed, ref } from 'vue'

import { useImageViewerStore } from '@/stores/imageViewerStore'
import UserAvatar from '../common/UserAvatar.vue'
import CommentSection from './comment/CommentSection.vue'
import ReactionButton from './ReactionButton.vue'

const imageViewerStore = useImageViewerStore()
const toast = useCustomToast()
const postStore = usePostStore()

const props = defineProps({
    id: {
        type: String,
        require: true,
    },
    content: {
        type: String,
    },
    imageUrls: {
        type: Array,
    },
    reactions: {
        type: Object,
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
        type: [Date, String],
    },
    stats: {
        type: Object,
        required: true,
        default: () => ({ reactionCount: 0, commentCount: 0, shareCount: 0 }),
    },
    userActions: {
        type: Object,
        default: () => ({ reactionType: Number, isSaved: false }),
    },
})

const actionMenu = ref()
const loading = useLoadingStore()
const maxVisibleImages = 4

const visibleImages = computed(() => props.imageUrls.slice(0, maxVisibleImages))
// const isPostOwner = computed(() => user.userId === props.creator.id)
const showComments = ref(false)

const actionItems = [
    {
        label: 'Delete',
        icon: 'pi pi-trash',
        command: async () => {
            loading.show()
            await postStore.deletePost(props.id)
            loading.hide()
            toast.showCustomSuccess('Deleted success!')
        },
    },
]

const gridColsClass = computed(() => {
    const length = props.imageUrls.length
    if (length === 1) return 'grid-cols-1'
    if (length === 2) return 'grid-cols-2'
    if (length === 3) return 'grid-cols-3 grid-rows-2'
    return 'grid-cols-2 grid-rows-2'
})

const imageWrapperClass = (index) => {
    if (visibleImages.value.length === 3 && index === 0) {
        return 'col-span-2 row-span-2'
    }
    return ''
}

const imageHeightClass = computed(() => {
    return props.imageUrls.length > 3 ? 'max-h-[360px]' : 'max-h-[660px]'
})

const openModal = (index) => {
    imageViewerStore.openViewer(props.imageUrls, index)
}

const toggleMenuAction = (event) => {
    actionMenu.value.toggle(event)
}

const handleShowComment = () => (showComments.value = !showComments.value)
</script>

<style scoped>
.last-image-overlay {
    grid-row: span 2;
    grid-column: span 2;
}
</style>
