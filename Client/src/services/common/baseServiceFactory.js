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

// Utility to handle FormData
const createFormData = (data) => {
    if (data instanceof FormData) return data
    const formData = new FormData()
    Object.entries(data).forEach(([key, value]) => {
        if (value !== null && value !== undefined) {
            if (value instanceof File) {
                formData.append(key, value)
            } else if (typeof value === 'object') {
                formData.append(key, JSON.stringify(value))
            } else {
                formData.append(key, value)
            }
        }
    })
    return formData
}

export const createBaseService = (instance) => {
    const makeRequest = async (method, url = '', data = null, params = null, config = {}) => {
        const finalUrl = url ? `/${url}` : ''

        const requestConfig = {
            method,
            url: finalUrl,
            paramsSerializer: {
                serialize: (params) => encodeQueryParams(params).toString(),
            },
            ...(data && { data }),
            ...(params && { params }),
            ...config,
        }

        return instance(requestConfig)
    }

    return {
        healthCheck: () => makeRequest('get', 'health-check'),

        create: (data) => makeRequest('post', '', data),

        getAll: (params = {}) => makeRequest('get', '', null, params),

        delete: (id) => makeRequest('delete', id),

        update: (id, data) => makeRequest('put', id, data),

        getById: (id) => makeRequest('get', id),

        // File upload methods
        upload: (endpoint, data) => {
            const formData = createFormData(data)
            return makeRequest('post', endpoint, formData, null, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            })
        },

        updateWithFile: (endpoint, data) => {
            const formData = createFormData(data)
            return makeRequest('put', endpoint, formData, null, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            })
        },

        // Additional utility methods
        patch: (id, data) => makeRequest('patch', id, data),

        patchWithFile: (endpoint, data) => {
            const formData = createFormData(data)
            return makeRequest('patch', endpoint, formData, null, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            })
        },

        head: (url) => makeRequest('head', url),

        options: (url) => makeRequest('options', url),

        // Custom request methods for flexibility
        request: (method, url, data, params, config) =>
            makeRequest(method, url, data, params, config),

        uploadRequest: (method, url, data, config = {}) => {
            const formData = createFormData(data)
            return makeRequest(method, url, formData, null, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
                ...config,
            })
        },
    }
}
