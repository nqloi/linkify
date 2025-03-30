<template>
    <Dialog
        v-model:visible="visible"
        :header="'Edit Profile'"
        :modal="true"
        class="w-full max-w-3xl"
    >
        <div class="p-fluid">
            <form @submit.prevent="handleSubmit">
                <!-- Basic Info -->
                <div class="mb-4">
                    <h3 class="text-lg font-semibold mb-3">Basic Information</h3>
                    <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
                        <div class="field">
                            <label for="firstName">First Name</label>
                            <InputText id="firstName" v-model="form.firstName" required />
                        </div>
                        <div class="field">
                            <label for="lastName">Last Name</label>
                            <InputText id="lastName" v-model="form.lastName" required />
                        </div>
                    </div>
                </div>

                <!-- Bio -->
                <div class="field mb-4">
                    <label for="bio">Bio</label>
                    <Textarea id="bio" v-model="form.bio" rows="3" autoResize />
                </div>

                <!-- Details -->
                <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
                    <div class="field">
                        <label for="work">Work</label>
                        <InputText id="work" v-model="form.work" placeholder="Where do you work?" />
                    </div>
                    <div class="field">
                        <label for="education">Education</label>
                        <InputText
                            id="education"
                            v-model="form.education"
                            placeholder="Where did you study?"
                        />
                    </div>
                    <div class="field">
                        <label for="location">Location</label>
                        <InputText
                            id="location"
                            v-model="form.location"
                            placeholder="Where do you live?"
                        />
                    </div>
                    <div class="field">
                        <label for="relationship">Relationship Status</label>
                        <Dropdown
                            id="relationship"
                            v-model="form.relationship"
                            :options="relationshipOptions"
                            placeholder="Select status"
                        />
                    </div>
                </div>
            </form>
        </div>

        <template #footer>
            <div class="flex justify-end gap-2">
                <Button label="Cancel" icon="pi pi-times" @click="closeModal" outlined />
                <Button label="Save" icon="pi pi-check" @click="handleSubmit" :loading="loading" />
            </div>
        </template>
    </Dialog>
</template>

<script setup>
import { ref, watchEffect } from 'vue'
import Dialog from 'primevue/dialog'
import InputText from 'primevue/inputtext'
import Textarea from 'primevue/textarea'
import Dropdown from 'primevue/dropdown'
import Button from 'primevue/button'

const props = defineProps({
    visible: Boolean,
    userProfile: {
        type: Object,
        required: true,
    },
})

const emit = defineEmits(['update:visible', 'save'])

const loading = ref(false)
const relationshipOptions = [
    'Single',
    'In a relationship',
    'Married',
    'Engaged',
    "It's complicated",
]

const form = ref({
    firstName: '',
    lastName: '',
    bio: '',
    work: '',
    education: '',
    location: '',
    relationship: '',
})

watchEffect(() => {
    if (props.visible && props.userProfile) {
        form.value = {
            firstName: props.userProfile.firstName || '',
            lastName: props.userProfile.lastName || '',
            bio: props.userProfile.bio || '',
            work: props.userProfile.work || '',
            education: props.userProfile.education || '',
            location: props.userProfile.location || '',
            relationship: props.userProfile.relationship || '',
        }
    }
})

const closeModal = () => {
    emit('update:visible', false)
}

const handleSubmit = async () => {
    try {
        loading.value = true
        await emit('save', form.value)
        closeModal()
    } catch (error) {
        console.error('Failed to save profile:', error)
    } finally {
        loading.value = false
    }
}
</script>
