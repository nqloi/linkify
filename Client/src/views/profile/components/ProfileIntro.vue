<template>
    <Card>
        <template #title>
            <div class="flex justify-between items-center">
                <span class="text-xl font-bold">Intro</span>
                <Button v-if="isCurrentUser" label="Edit" link @click="$emit('edit')" />
            </div>
        </template>
        <template #content>
            <div class="space-y-4">
                <!-- Bio -->
                <p v-if="userProfile.bio" class="text-gray-600">
                    {{ userProfile.bio }}
                </p>
                <p v-else>
                    <i class="text-gray-400">No bio added yet</i>
                </p>

                <!-- Info Items -->
                <div class="space-y-3">
                    <div v-if="userProfile.work" class="flex items-center gap-3">
                        <i class="pi pi-briefcase text-gray-600"></i>
                        <span>Works at {{ userProfile.work }}</span>
                    </div>

                    <div v-if="userProfile.education" class="flex items-center gap-3">
                        <i class="pi pi-book text-gray-600"></i>
                        <span>Studied at {{ userProfile.education }}</span>
                    </div>

                    <div v-if="userProfile.location" class="flex items-center gap-3">
                        <i class="pi pi-map-marker text-gray-600"></i>
                        <span>Lives in {{ userProfile.location }}</span>
                    </div>

                    <div v-if="userProfile.relationship" class="flex items-center gap-3">
                        <i class="pi pi-heart text-gray-600"></i>
                        <span>{{ userProfile.relationship }}</span>
                    </div>

                    <div class="flex items-center gap-3">
                        <i class="pi pi-clock text-gray-600"></i>
                        <span>Joined {{ joinedDate }}</span>
                    </div>
                </div>
            </div>
        </template>
    </Card>
</template>

<script setup>
import { computed } from 'vue'
import Card from 'primevue/card'
import Button from 'primevue/button'

const props = defineProps({
    userProfile: {
        type: Object,
        required: true,
    },
    isCurrentUser: {
        type: Boolean,
        default: false,
    },
})

const joinedDate = computed(() => {
    if (!props.userProfile.joinedAt) return ''
    return new Date(props.userProfile.joinedAt).toLocaleDateString('en-US', {
        month: 'long',
        year: 'numeric',
    })
})
</script>
