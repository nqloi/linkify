import TOAST_GROUP from '@/common/constants/toastGroup'
import { useToast } from 'primevue'

const useNotificationToast = () => {
    const toast = useToast()

    // Show a new notification toast
    const showToast = (notification) => {
        // Ensure notification has a valid ID
        const toastNotification = {
            severity: 'info',
            summary: notification.message,
            group: TOAST_GROUP.NOTIFICATION,
            detail: notification,
        }
        // Auto-remove after 6 seconds
        toast.add(toastNotification)
    }

    return {
        // actions
        showToast,
    }
}

export default useNotificationToast
