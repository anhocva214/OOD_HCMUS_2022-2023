import { UserRegister } from 'src/models/request/user'
import { User } from 'src/models/response/user.model'
import { axiosClient } from './axios-client'

const PATH = {
    register: '/register-user'
}

export function register(data: UserRegister){
    return axiosClient({
        method: 'post',
        url: PATH.register,
        data
    })
}