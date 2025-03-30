<template>
    <div
        class="flex flex-col rounded-lg w-full h-full bg-white sm:w-5/6 sm:h-5/6 md:w-4/5 md:h-4/5 lg:w-2/3 lg:h-2/3 p-4 md:p-6 lg:p-8 max-w-xl max-h-xl"
    >
        <div class="text-2xl mb-8">Welcome !</div>
        <div class="text-xl font-semibold mb-6">
            Sign in to <span class="text-primary">Linkify</span>
        </div>
        <div class="flex flex-col gap-2 mb-4">
            <label for="username">Username</label>
            <IconField>
                <InputIcon class="pi pi-user" />
                <InputText class="w-full" v-model="username" placeholder="Enter your username" />
            </IconField>
        </div>
        <div class="flex flex-col gap-2 mb-4">
            <label for="password">Password</label>
            <IconField>
                <InputIcon class="pi pi-lock" />
                <InputText class="w-full" v-model="password" placeholder="Enter your password" />
            </IconField>
        </div>

        <div class="flex justify-end mb-6">
            <div class="cursor-pointer" :onclick="handleForgotPassword">Forgot password?</div>
        </div>
        <Button label="Login" class="w-full mb-10" :onclick="handleLogin" />
        <div class="flex justify-center">
            Don't have an Account?<span
                class="font-semibold cursor-pointer text-primary-500"
                :onclick="handleNavigateRegister"
                >&nbsp;Register</span
            >
        </div>
    </div>
</template>
<script setup>
import IconField from 'primevue/iconfield'
import InputIcon from 'primevue/inputicon'
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/authStore'
import { useCustomToast } from '@/utils/toast/customToast'
import exceptionType from '@/common/constants/exceptionType'

const toast = useCustomToast()
const route = useRouter()
const isRemember = ref(false)
const username = ref('guest')
const password = ref('123456@Abc')

const handleForgotPassword = () => {
    console.log('forgot pass')
}

const handleNavigateRegister = () => {
    route.push({
        name: 'Register',
    })
}

const handleLogin = async () => {
    const { login } = useAuthStore()
    try {
        const isAuthenticated = await login({
            username: username.value,
            password: password.value,
        })
        isAuthenticated && route.push('/')
    } catch (error) {
        if (error?.error?.exceptionType === exceptionType.UnauthorizedAccessException) {
            toast.showCustomError('User name or password is incorrect.')
        } else {
            toast.showCustomError()
        }
    }
}
</script>
<style scoped></style>
