const colors = {
    primary: 'var(--primary)',
    'primary-inverse': 'var(--primary-inverse)',
    'primary-hover': 'var(--primary-hover)',
    'primary-active-color': 'var(--primary-active-color)',

    'primary-highlight': 'var(--primary) / var(--primary-highlight-opacity)',
    'primary-highlight-inverse': 'var(--primary-highlight-inverse)',
    'primary-highlight-hover': 'var(--primary) / var(--primary-highlight-hover-opacity)',

    'primary-50': 'var(--primary-50)',
    'primary-100': 'var(--primary-100)',
    'primary-200': 'var(--primary-200)',
    'primary-300': 'var(--primary-300)',
    'primary-400': 'var(--primary-400)',
    'primary-500': 'var(--primary-500)',
    'primary-600': 'var(--primary-600)',
    'primary-700': 'var(--primary-700)',
    'primary-800': 'var(--primary-800)',
    'primary-900': 'var(--primary-900)',
    'primary-950': 'var(--primary-950)',

    'surface-0': 'var(--surface-0)',
    'surface-50': 'var(--surface-50)',
    'surface-100': 'var(--surface-100)',
    'surface-200': 'var(--surface-200)',
    'surface-300': 'var(--surface-300)',
    'surface-400': 'var(--surface-400)',
    'surface-500': 'var(--surface-500)',
    'surface-600': 'var(--surface-600)',
    'surface-700': 'var(--surface-700)',
    'surface-800': 'var(--surface-800)',
    'surface-900': 'var(--surface-900)',
}

export default {
    darkMode: 'class',
    content: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
    plugins: [require('tailwindcss-primeui')],
    theme: {
        extend: {
            colors,
        },
        screens: {
            sm: '576px',
            md: '768px',
            lg: '992px',
            xl: '1200px',
            '2xl': '1920px',
        },
    },
}
