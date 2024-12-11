<template>
    <aside class="sidebar-left h-full w-64 flex flex-col gap-3">
        <!-- User Information -->
        <SimpleCard>
            <div class="flex items-center">
                <Avatar icon="pi pi-user" size="large" shape="circle" class="mr-3" />
                <div>
                    <p class="text-base font-semibold">{{ user.name }}</p>
                    <p class="text-sm text-gray-500">{{ user.followers }} Followers</p>
                    <p class="text-sm text-gray-500">{{ user.following }} Following</p>
                </div>
            </div>
        </SimpleCard>

        <!-- Menu Items -->
        <SimpleCard>
            <ul class="menu">
                <li v-for="item in menuItems" :key="item.label" class="menu-item mb-2">
                    <router-link
                        :to="item.to"
                        class="flex items-center py-2 px-3 rounded-md hover:bg-primary-highlight"
                    >
                        <i :class="item.icon + ' text-lg mr-3 text-primary'"></i>
                        <span class="text-sm font-medium text-text-color">{{ item.label }}</span>
                        <span
                            v-if="item.badge"
                            class="ml-auto bg-primary text-primary-inverse text-xs rounded-full px-2 py-0.5"
                        >
                            {{ item.badge }}
                        </span>
                    </router-link>
                </li>
            </ul>
        </SimpleCard>

        <!-- Pages You Like -->
        <SimpleCard class="flex-1">
            <div class="pages-you-like mt-10">
                <h3 class="text-sm font-semibold text-text-color-secondary mb-3">Pages You Like</h3>
                <ul>
                    <li v-for="page in likedPages" :key="page.name" class="flex items-center mb-4">
                        <Avatar :image="page.avatar" size="large" shape="circle" />
                        <div class="ml-3">
                            <p class="text-sm font-medium text-text-color">{{ page.name }}</p>
                            <p class="text-xs text-text-color-secondary">{{ page.category }}</p>
                        </div>
                    </li>
                </ul>
            </div>
        </SimpleCard>
    </aside>
</template>

<script setup>
import SimpleCard from '@/components/common/SimpleCard.vue'
import { useAuthStore } from '@/stores/authStore'
import { Avatar, Card } from 'primevue'

const user = {
    name: 'Jakob Botosh',
    username: 'jakobbts',
    avatar: '',
    followers: '2,3k',
    following: '5k',
}

const menuItems = [
    { label: 'Feed', icon: 'pi pi-home', to: '/' },
    { label: 'Friends', icon: 'pi pi-users', to: '/' },
    { label: 'Event', icon: 'pi pi-calendar', to: '/', badge: 4 },
    { label: 'Watch Videos', icon: 'pi pi-video', to: '/' },
    { label: 'Photos', icon: 'pi pi-image', to: '/' },
    { label: 'Marketplace', icon: 'pi pi-shopping-cart', to: '/' },
    { label: 'Files', icon: 'pi pi-folder', to: '/', badge: 7 },
]

const likedPages = [
    {
        name: 'UI/UX Community',
        category: 'Design',
        avatar: '/avatars/uiux.png',
    },
    {
        name: 'Web Designer',
        category: 'Development',
        avatar: '/avatars/webdesigner.png',
    },
    {
        name: 'Dribbble Community',
        category: 'Design',
        avatar: '/avatars/dribbble.png',
    },
    {
        name: 'Behance',
        category: 'Design',
        avatar: '/avatars/behance.png',
    },
]
</script>

<style scoped lang="scss">
.dark-card {
    @apply dark:bg-surface-0 dark:text-white;
}

.sidebar-left {
    background-color: var(--p-surface-card);
    color: var(--text-color);

    .user-info {
        background-color: var(--primary-50);
    }
    .menu {
        .menu-item {
            margin-bottom: 0.5rem;

            a {
                transition: background-color 0.2s ease;
            }
        }
    }

    .pages-you-like {
        li {
            p {
                margin: 0;
            }
        }
    }
}
</style>
