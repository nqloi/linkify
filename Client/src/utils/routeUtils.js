export const encodePathSegment = (segment) => {
    if (segment === null || segment === undefined) return ''
    return encodeURIComponent(String(segment).trim())
}

export const createResourcePath = (...parts) => {
    return parts
        .filter(Boolean)
        .map((part) => encodePathSegment(part))
        .join('/')
}
