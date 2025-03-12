import useAxios from '../common/useAxios'

const useReactionService = (postId) => {
    const controller = `posts/${postId}/reactions`

    const { baseService } = useAxios(controller)

    const addOrUpdate = (type) => {
        const { baseService } = useAxios(controller)

        return baseService.create({
            type,
        })
    }

    return {
        ...baseService,
        addOrUpdate,
    }
}

export default useReactionService
