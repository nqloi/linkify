<template>
    <div class="story-bar">
        <Carousel
            :value="visibleStories"
            :numVisible="5"
            :numScroll="1"
            orientation="horizontal"
            :circular="true"
            :showIndicators="false"
            @onPage="loadMoreStories"
        >
            <template #item="slotProps">
                <div class="story-item flex flex-col items-center cursor-pointer">
                    <Avatar
                        icon="pi pi-user"
                        shape="circle"
                        class="w-16 h-16 mb-2 border-2 border-blue-500"
                    />
                    <p class="text-sm font-medium text-center">{{ slotProps.data.name }}</p>
                </div>
            </template>
        </Carousel>
    </div>
</template>

<script setup>
import { ref } from 'vue'
import { Carousel } from 'primevue'
import { Avatar } from 'primevue'

// Danh sách toàn bộ story
const allStories = ref([
    { id: 1, name: 'Your Story', avatar: 'path_to_avatar1' },
    { id: 2, name: 'Justin Rosser', avatar: 'path_to_avatar2' },
    { id: 3, name: 'Davis Donwart', avatar: 'path_to_avatar3' },
    { id: 4, name: 'Randy Saris', avatar: 'path_to_avatar4' },
    { id: 5, name: 'Charlie Press', avatar: 'path_to_avatar5' },
    { id: 6, name: 'Talan Philips', avatar: 'path_to_avatar6' },
    { id: 7, name: 'Corey Gouse', avatar: 'path_to_avatar7' },
    { id: 8, name: 'New User 1', avatar: 'path_to_avatar8' },
    { id: 9, name: 'New User 2', avatar: 'path_to_avatar9' },
    { id: 10, name: 'New User 3', avatar: 'path_to_avatar10' },
])

const visibleStories = ref(allStories.value.slice(0, 5))

const loadMoreStories = () => {
    const nextIndex = visibleStories.value.length
    const nextStories = allStories.value.slice(nextIndex, nextIndex + 5)

    // Check if there are new stories
    if (nextStories.length > 0) {
        visibleStories.value = [...visibleStories.value, ...nextStories]
    } else {
        // If story runs out, repeat (create infinite loop)
        visibleStories.value = [...visibleStories.value, ...allStories.value]
    }
}
</script>

<style scoped>
.story-bar {
    background-color: white;
    padding: 1rem;
    border-radius: 8px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

.story-item {
    text-align: center;
}

.story-item img {
    transition: transform 0.2s;
}

.story-item:hover img {
    transform: scale(1.1);
}
</style>
