import { useToast } from 'primevue/usetoast'

export const useCustomToast = () => {
    const toast = useToast()

    const showCustomSuccess = (message) => {
        toast.add({
            severity: 'success',
            summary: 'Success',
            detail: message,
            life: 3000,
        })
    }

    const showCustomError = (message = 'An error occurred') => {
        toast.add({
            severity: 'error',
            summary: 'Error',
            detail: message,
            life: 5000,
        })
    }

    return {
        ...toast, // Kế thừa các method gốc từ useToast
        showCustomSuccess,
        showCustomError,
    }
}
