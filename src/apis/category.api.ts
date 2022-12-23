import { Category } from "src/models/response/category.model"
import { axiosClient } from "./axios-client"


const PATH = {
    getAll: '/get-categories',
    create: '/add-categories'
}

function getAllCategories(){
    return axiosClient<Category[]>({
        url: PATH.getAll,
        method: 'GET'
    })
}

function createCategory(data: Category){
    return axiosClient({
        url: PATH.create,
        method: 'POST',
        data
    })
}

export const categoryApi = {
    getAllCategories,
    createCategory
}