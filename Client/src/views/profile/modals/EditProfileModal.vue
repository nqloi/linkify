<template>
    <Dialog
        :visible="isVisible"
        @update:visible="$emit('update:visible', $event)"
        :header="'Edit Profile'"
        :modal="true"
        class="w-full max-w-3xl"
    >
        <div class="p-fluid">
            <Form :resolver="formResolver" @submit="handleSubmit">
                <!-- Basic Info -->
                <div class="mb-4">
                    <h3 class="text-lg font-semibold mb-3">Basic Information</h3>
                    <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
                        <div class="field">
                            <label for="firstName">First Name</label>
                            <FormField
                                v-slot="$field"
                                name="firstName"
                                :resolver="firstNameResolver"
                                :initialValue="userProfile.firstName || ''"
                            >
                                <InputText id="firstName" fluid />
                                <Message
                                    v-if="$field?.invalid && $field?.error"
                                    severity="error"
                                    size="small"
                                    variant="simple"
                                    >{{ $field?.error?.message }}</Message
                                >
                            </FormField>
                        </div>
                        <div class="field">
                            <label for="lastName">Last Name</label>
                            <FormField
                                v-slot="$field"
                                name="lastName"
                                :resolver="lastNameResolver"
                                :initialValue="userProfile.lastName || ''"
                            >
                                <InputText id="lastName" fluid />
                                <Message
                                    v-if="$field?.invalid && $field?.error"
                                    severity="error"
                                    size="small"
                                    variant="simple"
                                    >{{ $field?.error?.message }}</Message
                                >
                            </FormField>
                        </div>
                    </div>
                </div>

                <!-- Bio -->
                <div class="field mb-4">
                    <label for="bio">Bio</label>
                    <FormField
                        v-slot="$field"
                        name="bio"
                        :resolver="bioResolver"
                        :initialValue="userProfile.bio || ''"
                    >
                        <Textarea id="bio" rows="3" autoResize fluid />
                        <Message
                            v-if="$field?.invalid && $field?.error"
                            severity="error"
                            size="small"
                            variant="simple"
                            >{{ $field?.error?.message }}</Message
                        >
                        <small class="text-gray-500">{{ bioLength }}/500 characters</small>
                    </FormField>
                </div>

                <!-- Contact Info -->
                <div class="mb-4">
                    <h3 class="text-lg font-semibold mb-3">Contact Information</h3>
                    <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
                        <div class="field">
                            <label for="email">Email</label>
                            <FormField
                                v-slot="$field"
                                name="email"
                                :resolver="emailResolver"
                                :initialValue="userProfile.email || ''"
                            >
                                <InputText id="email" placeholder="Your email address" fluid />
                                <Message
                                    v-if="$field?.invalid && $field?.error"
                                    severity="error"
                                    size="small"
                                    variant="simple"
                                    >{{ $field?.error?.message }}</Message
                                >
                            </FormField>
                        </div>
                        <div class="field">
                            <label for="phone">Phone</label>
                            <FormField
                                v-slot="$field"
                                name="phone"
                                :resolver="phoneResolver"
                                :initialValue="userProfile.phone || ''"
                            >
                                <InputText id="phone" placeholder="Your phone number" fluid />
                                <Message
                                    v-if="$field?.invalid && $field?.error"
                                    severity="error"
                                    size="small"
                                    variant="simple"
                                    >{{ $field?.error?.message }}</Message
                                >
                            </FormField>
                        </div>
                    </div>
                </div>

                <!-- Details -->
                <div class="mb-4">
                    <h3 class="text-lg font-semibold mb-3">Personal Details</h3>
                    <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
                        <div class="field">
                            <label for="work">Work</label>
                            <FormField name="work" :initialValue="userProfile.work || ''">
                                <InputText id="work" placeholder="Where do you work?" fluid />
                            </FormField>
                        </div>
                        <div class="field">
                            <label for="education">Education</label>
                            <FormField name="education" :initialValue="userProfile.education || ''">
                                <InputText
                                    id="education"
                                    placeholder="Where did you study?"
                                    fluid
                                />
                            </FormField>
                        </div>
                        <div class="field">
                            <label for="location">Location</label>
                            <FormField name="location" :initialValue="userProfile.location || ''">
                                <InputText id="location" placeholder="Where do you live?" fluid />
                            </FormField>
                        </div>
                        <div class="field">
                            <label for="relationship">Relationship Status</label>
                            <FormField
                                name="relationship"
                                :initialValue="userProfile.relationship || ''"
                            >
                                <Dropdown
                                    id="relationship"
                                    :options="relationshipOptions"
                                    placeholder="Select status"
                                    fluid
                                />
                            </FormField>
                        </div>
                        <div class="field">
                            <label for="website">Website</label>
                            <FormField
                                v-slot="$field"
                                name="website"
                                :resolver="websiteResolver"
                                :initialValue="userProfile.website || ''"
                            >
                                <InputText id="website" placeholder="Your website URL" fluid />
                                <Message
                                    v-if="$field?.invalid && $field?.error"
                                    severity="error"
                                    size="small"
                                    variant="simple"
                                    >{{ $field?.error?.message }}</Message
                                >
                            </FormField>
                        </div>
                        <div class="field">
                            <label for="birthday">Birthday</label>
                            <FormField name="birthday" :initialValue="userProfile.birthday || ''">
                                <Calendar
                                    id="birthday"
                                    placeholder="Select your birthday"
                                    dateFormat="dd/mm/yy"
                                    fluid
                                />
                            </FormField>
                        </div>
                    </div>
                </div>

                <div class="flex justify-end gap-2">
                    <Button
                        type="button"
                        label="Cancel"
                        icon="pi pi-times"
                        @click="closeModal"
                        outlined
                    />
                    <Button type="submit" label="Save" icon="pi pi-check" :loading="loading" />
                </div>
            </Form>
        </div>
    </Dialog>
</template>

<script setup>
import { ref, computed } from 'vue'
import Dialog from 'primevue/dialog'
import InputText from 'primevue/inputtext'
import Textarea from 'primevue/textarea'
import Dropdown from 'primevue/dropdown'
import Button from 'primevue/button'
import Calendar from 'primevue/calendar'
import { Form, FormField } from '@primevue/forms'
import Message from 'primevue/message'
import { yupResolver } from '@primevue/forms/resolvers/yup'
import * as yup from 'yup'
import { useCustomToast } from '@/utils/toast/customToast'

const props = defineProps({
    visible: Boolean,
    userProfile: {
        type: Object,
        required: true,
    },
})

const emit = defineEmits(['update:visible', 'save'])

const toast = useCustomToast()
const loading = ref(false)
const relationshipOptions = [
    'Single',
    'In a relationship',
    'Married',
    'Engaged',
    "It's complicated",
]

// Form validation schemas
const firstNameResolver = yupResolver(yup.string().required('First name is required'))

const lastNameResolver = yupResolver(yup.string().required('Last name is required'))

const bioResolver = yupResolver(yup.string().max(500, 'Bio must be less than 500 characters'))

const phoneResolver = yupResolver(
    yup
        .string()
        .matches(/^\+?[\d\s-]{10,}$/, 'Invalid phone number format')
        .nullable(),
)

const emailResolver = yupResolver(yup.string().email('Invalid email format').nullable())

const websiteResolver = yupResolver(yup.string().url('Invalid website URL').nullable())

// Form schema for the entire form
const formSchema = yup.object().shape({
    firstName: yup.string().required('First name is required'),
    lastName: yup.string().required('Last name is required'),
    bio: yup.string().max(500, 'Bio must be less than 500 characters'),
    phone: yup
        .string()
        .matches(/^\+?[\d\s-]{10,}$/, 'Invalid phone number format')
        .nullable(),
    email: yup.string().email('Invalid email format').nullable(),
    website: yup.string().url('Invalid website URL').nullable(),
})

const formResolver = yupResolver(formSchema)

// Bio character counter
const bioLength = computed(() => {
    return props.userProfile?.bio?.length || 0
})

// Computed property for visible prop
const isVisible = computed({
    get: () => props.visible,
    set: (value) => emit('update:visible', value),
})

const closeModal = () => {
    emit('update:visible', false)
}

const handleSubmit = async ({ values, valid }) => {
    if (!valid) {
        toast.showError('Please correct the errors in the form')
        return
    }

    try {
        loading.value = true
        await emit('save', values)
        toast.showSuccess('Profile updated successfully')
        closeModal()
    } catch (error) {
        toast.showError('Failed to save profile')
        console.error('Failed to save profile:', error)
    } finally {
        loading.value = false
    }
}
</script>

<style scoped>
:deep(.p-dropdown) {
    width: 100%;
}

:deep(.p-calendar) {
    width: 100%;
}
</style>
