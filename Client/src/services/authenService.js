import useAxios from './common/useAxios'

const controller = 'auth'
const { instance, baseService } = useAxios(controller)

export const authService = {
  ...baseService,
  login(credentials) {
    return instance.post('/login', credentials)
  },
  logout() {
    return instance.post('/logout')
  },
  register(registerInfo) {
    return instance.post('/register', registerInfo)
  }
}
