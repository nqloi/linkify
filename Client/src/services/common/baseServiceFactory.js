/**
 * Utility function to encode query value
 * @param {*} value
 * @returns {string}
 */
const encodeQueryValue = (value) => {
    if (value === null || value === undefined) {
        return ''
    }

    if (Array.isArray(value)) {
        return value.map((item) => encodeQueryValue(item))
    }

    if (value instanceof Date) {
        return value.toISOString()
    }

    if (typeof value === 'object' && value !== null) {
        return JSON.stringify(value)
    }

    return String(value)
}

/**
 * Utility function to encode query parameters for URL
 * @param {Object} params
 * @returns {URLSearchParams}
 */
const encodeQueryParams = (params = {}) => {
    const searchParams = new URLSearchParams()

    const appendValue = (key, value) => {
        if (value === null || value === undefined) return

        if (Array.isArray(value)) {
            value.forEach((item) => {
                const encodedValue = encodeQueryValue(item)
                searchParams.append(`${key}[]`, encodedValue)
            })
            return
        }

        const encodedValue = encodeQueryValue(value)
        searchParams.set(key, encodedValue)
    }

    Object.entries(params).forEach(([key, value]) => appendValue(key, value))
    return searchParams
}

export const createBaseService = (instance) => {
    const makeRequest = async (method, url = '', data = null, params = null) => {
        const finalUrl = url ? `/${url}` : ''

        const config = {
            method,
            url: finalUrl,
            paramsSerializer: {
                serialize: (params) => encodeQueryParams(params).toString(),
            },
            ...(data && { data }),
            ...(params && { params }), // Pass params directly, let axios handle with paramsSerializer
        }

        return instance(config)
    }

    return {
        healthCheck: () => makeRequest('get', 'health-check'),

        create: (data) => makeRequest('post', '', data),

        getAll: (params = {}) => makeRequest('get', '', null, params),

        delete: (id) => makeRequest('delete', id),

        update: (id, data) => makeRequest('put', id, data),

        getById: (id) => makeRequest('get', id),

        // Additional utility methods
        patch: (id, data) => makeRequest('patch', id, data),

        head: (url) => makeRequest('head', url),

        options: (url) => makeRequest('options', url),

        // Custom request method for flexibility
        request: (method, url, data, params) => makeRequest(method, url, data, params),
    }
}
