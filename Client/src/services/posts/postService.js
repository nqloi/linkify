import useAxios from '../common/useAxios'

const usePostService = () => {
    const controller = 'post'
    const { baseService } = useAxios(controller)

    const create = (data) => {
        const { baseService } = useAxios(controller, 'v1', {
            'Content-Type': 'multipart/form-data',
        })
        return baseService.create(data)
    }
    return {
        ...baseService,
        create,
    }
}

export default usePostService
