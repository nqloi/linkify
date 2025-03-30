const SIZE = {
    XS: 'xs', // Extra Small
    SM: 'sm', // Small
    MD: 'md', // Medium
    LG: 'lg', // Large
    XL: 'xl', // Extra Large
    XXL: 'xxl', // Extra Extra Large (you can add more if needed)
    XXXl: 'xxxl',
}

const TAILWIND_SIZE_MAP = {
    [SIZE.XS]: 'w-6 h-6',
    [SIZE.SM]: 'w-8 h-8',
    [SIZE.MD]: 'w-10 h-10',
    [SIZE.LG]: 'w-12 h-12',
    [SIZE.XL]: 'w-16 h-16',
    [SIZE.XXL]: 'w-20 h-20',
    [SIZE.XXXl]: 'w-24 h-24',
}

export { SIZE, TAILWIND_SIZE_MAP }
