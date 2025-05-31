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
import { DEBUG_MODE } from './utils/envConfig'

import App from './App.vue'
import router from './router'

// Disable console logging in production when debug mode is off
if (!DEBUG_MODE) {
    console.log = () => {}
    console.debug = () => {}
    // Keep error and warn for production troubleshooting
}

const app = createApp(App)

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
    ripple: true,
    zIndex: {
        modal: 1100, // Dialog, Sidebar
        overlay: 1000, // Dropdown, Tooltip
        menu: 1000, // Overlay Menus
        tooltip: 1100, // Tooltip
        toast: 1200, // Toast
    },
})
app.use(ConfirmationService)
app.use(ToastService, {
    position: 'bottom-right',
    group: {
        notification: {
            position: 'bottom-right',
        },
    },
})
app.use(router)

app.component('Button', Button)
app.component('Dialog', Dialog)
app.component('InputText', InputText)
app.component('FloatLabel', FloatLabel)

app.directive('tooltip', Tooltip)

app.mount('#app')
