import useAxios from '../common/useAxios'

const useUserProfileService = () => {
    const controller = `user-profiles`
    const { baseService } = useAxios(controller)

    return {
        ...baseService,
    }
}

export default useUserProfileService
