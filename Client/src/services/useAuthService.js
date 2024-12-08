import useAxios from './common/useAxios'

const useAuthService = () => {
    const controller = 'auth'
    const { instance, baseService } = useAxios(controller)

    return {
        ...baseService,
        login: (credentials) => instance.post('/login', credentials),
        logout: () => instance.post('/logout'),
        register: (registerInfo) => instance.post('/register', registerInfo),
        checkDuplicateUsername: (username) => instance.post('/check-username', username),
    }
}

export default useAuthService
