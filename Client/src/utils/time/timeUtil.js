import moment from 'moment'

export function timeAgo(dateString) {
    let date = moment.utc(dateString)

    return date.fromNow()
}
