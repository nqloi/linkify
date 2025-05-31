import { ref, onMounted } from 'vue'
import { logger } from '@/utils/logger'

export function useDarkMode() {
    const isDarkMode = ref(false)

    /**
     * Initialize dark mode from localStorage or system preference
     */
    const initDarkMode = () => {
        try {
            // Initialize dark mode from localStorage or system preference
            const savedDarkMode = localStorage.getItem('darkMode')
            if (savedDarkMode !== null) {
                isDarkMode.value = savedDarkMode === 'true'
            } else {
                isDarkMode.value = window.matchMedia('(prefers-color-scheme: dark)').matches
            }

            // Apply initial dark mode state
            applyDarkMode(isDarkMode.value)
        } catch (error) {
            logger.error('Error initializing dark mode:', error)
            isDarkMode.value = false
            applyDarkMode(false)
        }
    }

    /**
     * Toggle dark mode on/off
     */
    const toggleDarkMode = () => {
        isDarkMode.value = !isDarkMode.value
        applyDarkMode(isDarkMode.value)
        saveDarkModePreference(isDarkMode.value)
    }

    /**
     * Apply dark mode to the DOM
     */
    const applyDarkMode = (value) => {
        if (value) {
            document.documentElement.classList.add('dark')
        } else {
            document.documentElement.classList.remove('dark')
        }
    }

    /**
     * Save dark mode preference to localStorage
     */
    const saveDarkModePreference = (value) => {
        try {
            localStorage.setItem('darkMode', value ? 'true' : 'false')
        } catch (error) {
            logger.error('Error saving dark mode preference:', error)
        }
    }

    // Initialize on mount
    onMounted(initDarkMode)

    return {
        isDarkMode,
        toggleDarkMode,
    }
}
