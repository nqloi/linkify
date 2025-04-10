export const LogLevel = {
    INFO: 'info',
    WARN: 'warn',
    ERROR: 'error',
    DEBUG: 'debug',
}

const defaultConfig = {
    level: LogLevel.INFO,
    enableConsole: true,
    // Can be extended later for ELK or other systems
    providers: ['console'],
}

export const createLogger = (config = defaultConfig) => {
    const log = (level, message, details) => {
        if (config.enableConsole) {
            console[level](message, details)
        }
        // Future extension point for other logging systems
    }

    return {
        info: (message, details) => log(LogLevel.INFO, message, details),
        warn: (message, details) => log(LogLevel.WARN, message, details),
        error: (message, details) => log(LogLevel.ERROR, message, details),
        debug: (message, details) => log(LogLevel.DEBUG, message, details),
    }
}

// Create default logger instance
export const logger = createLogger()
