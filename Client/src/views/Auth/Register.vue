<template>
    <div
        class="flex flex-col p-4 md:p-6 lg:p-8 rounded-lg bg-white w-full min-h-full sm:w-5/6 sm:min-h-[85%] md:w-4/5 md:min-h-[80%] lg:w-2/3 lg:min-h-3/4 max-w-xl"
    >
        <div class="text-2xl mb-8">Welcome !</div>
        <div class="text-xl font-semibold mb-6">
            Sign in to <span class="text-primary">Linkify</span>
        </div>
        <Form :resolver @submit="handleRegister">
            <div class="flex-spacing">
                <label for="username">Username</label>
                <FormField
                    v-slot="$field"
                    name="username"
                    :resolver="userNameResolver"
                    initialValue=""
                    :validateOnValueUpdate="false"
                    :validateOnBlur="true"
                >
                    <IconField>
                        <InputIcon class="pi pi-user" />
                        <InputText type="text" placeholder="Enter your username" fluid />
                    </IconField>
                    <Message
                        :v-if="$field?.invalid"
                        severity="error"
                        size="small"
                        variant="simple"
                        >{{ $field?.error?.message }}</Message
                    >
                </FormField>
            </div>
            <div class="flex gap-3 mb-4">
                <div class="flex-1 flex flex-col">
                    <label for="firstName">First Name</label>
                    <FormField
                        v-slot="$field"
                        name="firstName"
                        :resolver="firstNameResolver"
                        initialValue=""
                    >
                        <InputText type="text" placeholder="Enter your first name" fluid />
                        <Message
                            :v-if="$field?.invalid"
                            severity="error"
                            size="small"
                            variant="simple"
                            >{{ $field?.error?.message }}</Message
                        >
                    </FormField>
                </div>
                <div class="flex-1 flex flex-col">
                    <label for="lastName">Last Name</label>
                    <FormField
                        v-slot="$field"
                        name="lastName"
                        :resolver="lastNameResolver"
                        initialValue=""
                    >
                        <InputText type="text" placeholder="Enter your last name" fluid />
                        <Message
                            :v-if="$field?.invalid"
                            severity="error"
                            size="small"
                            variant="simple"
                            >{{ $field?.error?.message }}</Message
                        >
                    </FormField>
                </div>
            </div>
            <div class="flex-spacing">
                <label for="password">Password</label>
                <FormField v-slot="$field" name="password" initialValue="">
                    <Password :feedback="false" type="text" toggleMask fluid></Password>
                    <Message
                        :v-if="$field?.invalid"
                        severity="error"
                        size="small"
                        variant="simple"
                        >{{ $field?.error?.message }}</Message
                    >
                </FormField>
            </div>
            <div class="flex-spacing">
                <label for="confirmPassword">Confirm Password</label>
                <FormField v-slot="$field" name="confirmPassword" initialValue="">
                    <Password
                        type="text"
                        name="confirmPassword"
                        :feedback="false"
                        toggleMask
                        fluid
                    />
                    <Message
                        :v-if="$field?.invalid"
                        severity="error"
                        size="small"
                        variant="simple"
                        >{{ $field?.error?.message }}</Message
                    >
                </FormField>
            </div>
            <div class="flex justify-between mb-6">
                <div class="cursor-pointer" :onclick="handleForgotPassword">Forgot password?</div>
            </div>
            <Button type="submit" label="Register" class="w-full mb-10" />
            <div class="flex justify-center">
                Already have an Account?<span
                    class="font-semibold cursor-pointer text-primary-500"
                    :onclick="handleNavigateLogin"
                    >&nbsp;Login</span
                >
            </div>
        </Form>
    </div>
</template>

<script setup>
import IconField from 'primevue/iconfield'
import InputIcon from 'primevue/inputicon'
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import Password from 'primevue/password'
import { Form, FormField } from '@primevue/forms'
import Message from 'primevue/message'
import { yupResolver } from '@primevue/forms/resolvers/yup'
import * as yup from 'yup'
import useAuthService from '@/services/useAuthService'
import { useToast } from 'primevue'
import { useAuthStore } from '@/stores/authStore'

const route = useRouter()
const toast = useToast()
const label = {
    username: 'Username',
    password: 'Password',
    confirmPassword: 'Confirm password',
    firstName: 'First name',
    lastName: 'Last name',
}

const getMsgRequired = (label) => `${label} is required.`
const userNameResolver = yupResolver(
    yup
        .string()
        .required(getMsgRequired(label.username))
        .test('unique-username', 'Username already exists', async (value) => {
            const { checkDuplicateUsername } = useAuthService()
            try {
                const res = await checkDuplicateUsername(value)
                return !res?.content
            } catch (error) {
                return false
            }
        }),
)

const firstNameResolver = yupResolver(yup.string().required(getMsgRequired(label.firstName)))
const lastNameResolver = yupResolver(yup.string().required(getMsgRequired(label.lastName)))

const resolver = yupResolver(
    yup.object().shape({
        password: yup
            .string()
            .min(8, 'Password must have at least 8 characters')
            .matches(/[A-Z]/, 'Password must contain at least one uppercase letter')
            .matches(
                /[!@#$%^&*(),.?":{}|<>]/,
                'Password must contain at least one special character',
            )
            .matches(/\d/, 'Password must contain at least one digit')
            .matches(/[a-z]/, 'Password must contain at least one lowercase letter'),
        confirmPassword: yup.string().oneOf([yup.ref('password'), null], 'Password must match'),
    }),
)

const handleForgotPassword = () => {
    console.log('forgot pass')
}

const handleNavigateLogin = () => {
    route.push({
        name: 'Login',
    })
}

const handleRegister = async ({ values, valid }) => {
    if (!valid) return
    const { register } = useAuthService()
    const response = await register(values)
    if (response?.content) {
        const { login } = useAuthStore()
        const isAuthenticated = await login({
            username: values.username,
            password: values.password,
        })
        toast.add({
            severity: 'success',
            summary: 'Success',
            detail: 'Registration successful! You are now logged in.',
            life: 5000,
        })
        isAuthenticated && route.push('/')
    }
}
</script>
<style scoped>
.flex-spacing {
    @apply flex flex-col gap-2 mb-4;
}
</style>
