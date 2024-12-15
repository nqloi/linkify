<template>
    <div class="sidebar-right flex flex-col h-full">
        <!-- Messages Section -->
        <SimpleCard class="messages-section mb-3">
            <div class="header flex justify-between items-center mb-4">
                <h4 class="text-lg font-bold">Messages</h4>
                <Button icon="pi pi-pencil" class="p-button-text" />
            </div>
            <InputText placeholder="Search" class="w-full mb-4" />
            <div class="tabs flex justify-between text-sm text-gray-500 mb-2">
                <span class="font-semibold text-black">Primary</span>
                <span>General</span>
                <span class="text-blue-500">Requests(4)</span>
            </div>
            <ul>
                <li
                    v-for="message in messages"
                    :key="message.id"
                    class="flex items-center mb-4 cursor-pointer"
                >
                    <Avatar :image="message.avatar" shape="circle" class="mr-3" size="large" />
                    <div>
                        <h5 class="font-bold text-sm">{{ message.name }}</h5>
                    </div>
                </li>
            </ul>
            <Button label="View All" class="p-button-link text-blue-500 text-sm font-semibold" />
        </SimpleCard>

        <!-- Events Section -->
        <SimpleCard class="events-section flex-1">
            <div class="header flex justify-between items-center mb-4">
                <h4 class="text-lg font-bold">Events</h4>
                <Button icon="pi pi-ellipsis-h" class="p-button-text" />
            </div>
            <ul>
                <li v-for="event in events" :key="event.id" class="mb-4">
                    <div class="flex items-center text-sm">
                        <i class="pi pi-calendar mr-3"></i>
                        <div>
                            <h5 class="font-bold">{{ event.name }}</h5>
                            <p class="text-gray-500">{{ event.date }} - {{ event.location }}</p>
                        </div>
                    </div>
                </li>
            </ul>
        </SimpleCard>
    </div>
</template>

<script setup>
import SimpleCard from '@/components/common/SimpleCard.vue'
import { Avatar } from 'primevue'
import { ref } from 'vue'

const messages = ref([
    { id: 1, name: 'Roger Korsgaard', avatar: 'path_to_avatar_1' },
    { id: 2, name: 'Terry Torff', avatar: 'path_to_avatar_2' },
    { id: 3, name: 'Angel Bergson', avatar: 'path_to_avatar_3' },
    { id: 4, name: 'Emerson Gouse', avatar: 'path_to_avatar_4' },
    { id: 5, name: 'Corey Baptista', avatar: 'path_to_avatar_5' },
])

const events = ref([
    {
        id: 1,
        name: '10 Events Invites',
        date: 'Thu',
        location: 'Harpoon Mall, YK',
    },
    {
        id: 2,
        name: 'Web Dev 2.0 Meetup',
        date: 'Yoshkar-Ola, Russia',
        location: '',
    },
    {
        id: 3,
        name: "Prada's Invitation Birthday",
        date: '',
        location: '',
    },
])
</script>

<style scoped lang="scss">
.sidebar-right {
    width: 300px;
}

.header i {
    font-size: 1rem;
}

.events-section ul li {
    display: flex;
    align-items: start;
    gap: 0.75rem;
}
</style>
