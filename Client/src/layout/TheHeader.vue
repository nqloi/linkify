<template>
    <header class="h-16 bg-white dark:bg-slate-800 shadow-md fixed top-0 left-0 right-0 z-50">
        <div class="mx-auto h-full px-4">
            <div class="flex items-center h-full gap-4">
                <!-- Logo -->
                <Logo class="flex-shrink-0 mr-4" />

                <!-- Search Bar -->
                <div class="flex-grow max-w-xl mx-auto">
                    <div class="relative">
                        <input
                            v-model="searchText"
                            type="text"
                            placeholder="Search..."
                            class="w-full py-2 pl-10 pr-4 rounded-full bg-gray-100 dark:bg-slate-700 border border-transparent focus:border-primary-500 dark:focus:border-primary-400 focus:ring-2 focus:ring-primary-500/20 dark:focus:ring-primary-400/20 transition-colors duration-200"
                        />
                        <i
                            class="pi pi-search absolute left-3 top-1/2 -translate-y-1/2 text-gray-400"
                        ></i>
                    </div>
                </div>

                <!-- Right Actions -->
                <div class="flex items-center gap-2">
                    <!-- Dark Mode Toggle -->
                    <Button
                        @click="toggleDarkMode"
                        severity="secondary"
                        variant="outlined"
                        class="p-2 mr-24 rounded-full hover:bg-gray-100 dark:hover:bg-slate-700 transition-colors duration-200"
                        v-tooltip.left="isDarkMode ? 'Switch to Light Mode' : 'Switch to Dark Mode'"
                    >
                        <i class="pi" :class="isDarkMode ? 'pi-sun' : 'pi-moon'"></i>
                    </Button>

                    <div class="relative">
                        <Avatar
                            :image="profileStore.avatarUrl ?? defaultAvatar"
                            @click="toggleUserMenu"
                            class="h-10 w-10 cursor-pointer rounded-full"
                        />
                        <Menu ref="menu" :model="menuItems" :popup="true" />
                    </div>
                </div>
            </div>
        </div>
    </header>

    <!-- Spacer to prevent content from being hidden under fixed header -->
    <div class="h-16"></div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { useAuthStore } from '@/stores/authStore'
import { useProfileStore } from '@/stores/profileStore'
import Logo from '@/components/common/Logo.vue'
import defaultAvatar from '@/assets/images/avatar-default.svg'
import Menu from 'primevue/menu'
import router from '@/router'
import { Avatar } from 'primevue'

const authStore = useAuthStore()
const profileStore = useProfileStore()
const userProfile = computed(() => profileStore.currentUserProfile)
const menu = ref()
const searchText = ref('')
const isDarkMode = ref(false)

// Menu items for user dropdown
const menuItems = [
    {
        label: 'Profile',
        icon: 'pi pi-user',
        command: () => router.push(`/profile/${userProfile.value?.id}`),
    },
    {
        label: 'Settings',
        icon: 'pi pi-cog',
    },
    { separator: true },
    {
        label: 'Logout',
        icon: 'pi pi-power-off',
        command: () => {
            authStore.logout()
            router.push('/auth/login')
        },
    },
]

const toggleUserMenu = (event) => {
    menu.value?.toggle(event)
}

const toggleDarkMode = () => {
    isDarkMode.value = !isDarkMode.value
    document.documentElement.classList.toggle('dark')
    localStorage.setItem('darkMode', isDarkMode.value ? 'true' : 'false')
}

onMounted(() => {
    // Initialize dark mode from localStorage or system preference
    const savedDarkMode = localStorage.getItem('darkMode')
    if (savedDarkMode !== null) {
        isDarkMode.value = savedDarkMode === 'true'
    } else {
        isDarkMode.value = window.matchMedia('(prefers-color-scheme: dark)').matches
    }

    // Apply initial dark mode state
    if (isDarkMode.value) {
        document.documentElement.classList.add('dark')
    } else {
        document.documentElement.classList.remove('dark')
    }
})
</script>

<style lang="scss" scoped>
.pi {
    @apply text-gray-600 dark:text-gray-300;
}
</style>
