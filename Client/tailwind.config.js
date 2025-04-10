/** @type {import('tailwindcss').Config} */
export default {
    content: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
    darkMode: 'class',
    theme: {
        extend: {
            colors: {
                'text-color': 'var(--text-color)',
                'text-color-secondary': 'var(--text-color-secondary)',
                'surface-border': 'var(--surface-border)',
                'surface-card': 'var(--surface-card)',
                'surface-hover': 'var(--surface-hover)',
                'notification-bg': 'var(--notification-bg)',
                'notification-hover': 'var(--notification-hover)',
                'notification-unread': 'var(--notification-unread)',
                'notification-unread-hover': 'var(--notification-unread-hover)',
                'notification-divider': 'var(--notification-divider)',
                'bg-notification-unread': 'var(--notification-unread)',
                primary: {
                    50: 'var(--primary-50)',
                    100: 'var(--primary-100)',
                    200: 'var(--primary-200)',
                    300: 'var(--primary-300)',
                    400: 'var(--primary-400)',
                    500: 'var(--primary-500)',
                    600: 'var(--primary-600)',
                    700: 'var(--primary-700)',
                    800: 'var(--primary-800)',
                    900: 'var(--primary-900)',
                    950: 'var(--primary-950)',
                },
            },
        },
    },
}
