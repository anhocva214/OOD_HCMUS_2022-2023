import { UserForgotPassword, UserLogin, UserRegister } from 'src/models/request/user'
import { User, UserUpdate } from 'src/models/response/user.model'
import { axiosClient } from './axios-client'

const PATH = {
    register: '/register-user',
    login: '/login',
    forgotPassword: '/forgot-password',
    getUserById: (id: string) => `/get-user/${id}`,
    update: 'update-user'
}

function register(data: UserRegister){
    return axiosClient({
        method: 'POST',
        url: PATH.register,
        data
    })
}

function login(data: UserLogin){
    return axiosClient<User>({
        method: 'POST',
        url: PATH.login,
        data
    })
}

function forgotPassword(data: UserForgotPassword){
    return axiosClient<void>({
        method: 'PUT',
        url: PATH.forgotPassword,
        data
    })
}

function getUserById(id: string){
    return axiosClient<User>({
        method: 'GET',
        url: PATH.getUserById(id),
    })
}

function updateUser(data: UserUpdate){
    return axiosClient<User>({
        method: 'PUT',
        url: PATH.update,
        data
    })
}

export const userApi = {
    register,
    login,
    forgotPassword,
    getUserById,
    updateUser
}