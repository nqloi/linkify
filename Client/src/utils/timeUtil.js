import moment from 'moment'

export function timeAgo(dateString) {
    const date = moment.utc(dateString)
    const now = moment()

    const diffSeconds = now.diff(date, 'seconds')
    const diffMinutes = now.diff(date, 'minutes')
    const diffHours = now.diff(date, 'hours')
    const diffDays = now.diff(date, 'days')

    if (diffSeconds < 60) return 'Just now'
    if (diffMinutes < 60) return `${diffMinutes} minutes ago`
    if (diffHours < 24) return `${diffHours} hours ago`

    if (diffDays === 1) return `Yesterday at ${date.format('HH:mm')}`
    if (now.year() === date.year()) return date.format('MMM D [at] HH:mm')

    return date.format('MMM D, YYYY [at] HH:mm')
}
