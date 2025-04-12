/**
 * Environment variables utility for accessing configuration values
 * with proper fallbacks and validation
 */

// API Settings
export const API_URL = import.meta.env.VITE_API_URL || 'https://localhost:7029/api'
export const API_VERSION = import.meta.env.VITE_API_VERSION || 'v1'
export const NOTIFICATION_HUB_URL =
    import.meta.env.VITE_NOTIFICATION_HUB_URL || 'https://localhost:7029/hubs/notifications'

// App Settings
export const APP_TITLE = import.meta.env.VITE_APP_TITLE || 'Linkify'
export const DEBUG_MODE = import.meta.env.VITE_DEBUG === 'true' || import.meta.env.DEV

/**
 * Environment type helpers
 */
export const isDevelopment = import.meta.env.DEV
export const isProduction = import.meta.env.PROD

/**
 * Log environment configuration on startup in development mode
 */
if (DEBUG_MODE) {
    console.group('Environment Configuration')
    console.log('API URL:', API_URL)
    console.log('API Version:', API_VERSION)
    console.log('Notification Hub URL:', NOTIFICATION_HUB_URL)
    console.log('App Title:', APP_TITLE)
    console.log('Debug Mode:', DEBUG_MODE)
    console.log('Development Mode:', isDevelopment)
    console.log('Production Mode:', isProduction)
    console.groupEnd()
}
