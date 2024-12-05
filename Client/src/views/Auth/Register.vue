<template>
    <div class="flex flex-col p-8 rounded-lg bg-white w-2/3 h-3/4 max-w-xl">
        <div class="text-2xl mb-8">
            Welcome !
        </div>
        <div class="text-xl font-semibold mb-6">Sign in to <span class="text-primary">Linkify</span></div>
        <div class="flex-spacing">
            <label for="username">Username</label>
            <IconField>
                <InputIcon class="pi pi-user" />
                <InputText class="w-full" v-model="username" placeholder="Enter your username" />
            </IconField>
        </div>
        <div class="flex gap-2 mb-4">
            <div class="flex-1 flex flex-col">
                <label for="username">First Name</label>
                <IconField>
                    <InputIcon class="pi pi-user" />
                    <InputText class="w-full" v-model="firstName" placeholder="Enter your first name" />
                </IconField>
            </div>
            <div class="flex-1 flex flex-col">
                <label for="username">Last Name</label>
                <IconField>
                    <InputIcon class="pi pi-user" />
                    <InputText class="w-full" v-model="lastName" placeholder="Enter your last name" />
                </IconField>
            </div>
        </div>
        <div class="flex-spacing">
            <label for="password">Password</label>
            <IconField>
                <InputIcon class="pi pi-lock" />
                <InputText class="w-full" v-model="password" placeholder="Enter your Password" />
            </IconField>
        </div>
        <div class="flex-spacing">
            <label for="password">Confirm Password</label>
            <IconField>
                <InputIcon class="pi pi-lock" />
                <InputText class="w-full" v-model="confirmPassword" placeholder="Confirm your Password" />
            </IconField>
        </div>
        <div class="flex justify-between mb-6">
            <div class="cursor-pointer" :onclick="handleForgotPassword">
                Forgot password?
            </div>
        </div>
        <Button label="Register" class="w-full mb-10" :onclick="handleRegister" />
        <div class="flex justify-center">Already have an Account?<span
                class="font-semibold cursor-pointer text-primary-500" :onclick="handleNavigateLogin">&nbsp;Login</span>
        </div>
    </div>
</template>
<script setup>
import IconField from 'primevue/iconfield'
import InputIcon from 'primevue/inputicon'
import Checkbox from 'primevue/checkbox';
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const route = useRouter()
const isRemember = ref(false)
const firstName = ref('')
const lastName = ref('')
const username = ref('')
const password = ref('')
const confirmPassword = ref('')

const handleForgotPassword = () => {
    console.log("forgot pass")
}

const handleNavigateLogin = () => {
    route.push({
        name: 'Login'
    })
}

const handleRegister = async () => {
}
</script>
<style scoped>
.flex-spacing {
    @apply flex flex-col gap-2 mb-4;
}
</style>