<template>
    <div class="profile-navigation border-b border-gray-300 mb-4">
        <div class="max-w-7xl mx-auto">
            <TabMenu
                :model="menuItems"
                :activeIndex="activeIndex"
                @tab-change="handleTabChange"
                class="border-none"
            >
                <template #item="{ item }">
                    <div class="flex items-center gap-2 px-4 py-3 cursor-pointer">
                        <i :class="item.icon"></i>
                        <span>{{ item.label }}</span>
                        <Badge v-if="item.count" :value="item.count" />
                    </div>
                </template>
            </TabMenu>
        </div>
    </div>
</template>

<script setup>
import { ref, computed } from 'vue'
import TabMenu from 'primevue/tabmenu'
import Badge from 'primevue/badge'

const props = defineProps({
    activeTab: {
        type: String,
        default: 'posts',
    },
})

const emit = defineEmits(['update:tab'])

const menuItems = ref([
    { label: 'Posts', icon: 'pi pi-th-large', key: 'posts' },
    { label: 'About', icon: 'pi pi-user', key: 'about' },
    { label: 'Friends', icon: 'pi pi-users', key: 'friends' },
    { label: 'Photos', icon: 'pi pi-images', key: 'photos' },
    { label: 'Videos', icon: 'pi pi-video', key: 'videos' },
])

const activeIndex = computed(() => {
    return menuItems.value.findIndex((item) => item.key === props.activeTab)
})

const handleTabChange = (event) => {
    emit('update:tab', menuItems.value[event.index].key)
}
</script>

<style scoped>
.p-tabmenu .p-tabmenu-nav {
    border: none;
}

.p-tabmenu .p-tabmenu-nav .p-tabmenuitem.p-highlight .p-menuitem-link {
    border-bottom: 2px solid var(--primary-color);
}
</style>
