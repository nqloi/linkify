import useAxios from './common/useAxios'

const usePostService = () => {
    const controller = 'post'
    const { instance, baseService } = useAxios(controller)

    return {
        ...baseService,
    }
}

export default usePostService
