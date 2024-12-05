<template>
    <div class="flex flex-col pr-8 rounded-lg bg-white w-2/3 h-2/3 p-8 max-w-xl">
        <div class="text-2xl mb-8">
            Welcome !
        </div>
        <div class="text-xl font-semibold mb-6">Sign in to <span class="text-primary">Linkify</span></div>
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

        <div class="flex justify-between mb-6">
            <div class="flex items-center">
                <Checkbox inputId="remember-me" name="remember-me" v-model="isRemember" :binary="true" />
                <label for="remember-me" class="ml-2"> Remember me </label>
            </div>
            <div class="cursor-pointer" :onclick="handleForgotPassword">
                Forgot password?
            </div>
        </div>
        <Button label="Login" class="w-full mb-10" :onclick="handleLogin" />
        <div class="flex justify-center">Don't have an Account?<span
                class="font-semibold cursor-pointer text-primary-500"
                :onclick="handleNavigateRegister">&nbsp;Register</span>
        </div>
    </div>
</template>
<script setup>
import IconField from 'primevue/iconfield'
import InputIcon from 'primevue/inputicon'
import Checkbox from 'primevue/checkbox';
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useAuthStore } from '@/stores/authStore';

const route = useRouter()
const isRemember = ref(false)
const username = ref('guest')
const password = ref('123456@Abc')

const handleForgotPassword = () => {
    console.log("forgot pass")
}

const handleNavigateRegister = () => {
    route.push({
        name: 'Register'
    })
}

const handleLogin = async () => {
    const { login } = useAuthStore()
    const isAuthenticated = await login({
        username: username.value,
        password: password.value
    });
    isAuthenticated && route.push('/')
}
</script>
<style scoped></style>