import { UserLogin, UserRegister } from 'src/models/request/user'
import { User } from 'src/models/response/user.model'
import { axiosClient } from './axios-client'

const PATH = {
    register: '/register-user',
    login: '/login'
}

export function register(data: UserRegister){
    return axiosClient({
        method: 'post',
        url: PATH.register,
        data
    })
}

export function login(data: UserLogin){
    return axiosClient<User>({
        method: 'post',
        url: PATH.login,
        data
    })
}