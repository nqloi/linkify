import useAxios from '../common/useAxios'

const useCommentService = (postId) => {
    const controller = `post/${postId}/comment`

    const { baseService } = useAxios(controller)

    const create = (newComment) => {
        const { baseService } = useAxios(controller)

        return baseService.create(newComment)
    }

    return {
        ...baseService,
        create,
    }
}

export default useCommentService
