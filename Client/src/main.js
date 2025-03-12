import 'primeicons/primeicons.css'
import './assets/tailwind.css'
import './assets/scss/styles.scss'
import Aura from '@primevue/themes/aura'

import ToastService from 'primevue/toastservice'
import Button from 'primevue/button'
import InputText from 'primevue/inputtext'
import FloatLabel from 'primevue/floatlabel'
import ConfirmationService from 'primevue/confirmationservice'
import Dialog from 'primevue/dialog'
import Tooltip from 'primevue/tooltip'

import piniaPluginPersistedstate from 'pinia-plugin-persistedstate'
import { createApp } from 'vue'
import { createPinia } from 'pinia'
import PrimeVue from 'primevue/config'

import App from './App.vue'
import router from './router'
import { startChatService } from './services/chat/useChatService'
import { startNotificationService } from './services/notifications/useNotificationService'

const app = createApp(App)

// service
startChatService()
startNotificationService()

// store
const pinia = createPinia()
pinia.use(piniaPluginPersistedstate)

app.use(pinia)
app.use(PrimeVue, {
    theme: {
        preset: Aura,
        options: {
            cssLayer: {
                name: 'primevue',
                order: 'tailwind-base, primevue, tailwind-utilities',
            },
        },
    },
})
app.use(ConfirmationService)
app.use(ToastService)
app.use(router)

app.component('Button', Button)
app.component('Dialog', Dialog)
app.component('InputText', InputText)
app.component('FloatLabel', FloatLabel)

app.directive('tooltip', Tooltip)

app.mount('#app')
