import { UserForgotPassword, UserLogin, UserRegister } from 'src/models/request/user'
import { User } from 'src/models/response/user.model'
import { axiosClient } from './axios-client'

const PATH = {
    register: '/register-user',
    login: '/login',
    forgotPassword: '/forgot-password',
}

export function register(data: UserRegister){
    return axiosClient({
        method: 'POST',
        url: PATH.register,
        data
    })
}

export function login(data: UserLogin){
    return axiosClient<User>({
        method: 'POST',
        url: PATH.login,
        data
    })
}

export function forgotPassword(data: UserForgotPassword){
    return axiosClient<void>({
        method: 'PUT',
        url: PATH.forgotPassword,
        data
    })
}