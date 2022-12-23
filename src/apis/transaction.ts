import { TransactionCreate, TransactionUpdate } from "src/models/request/transaction.model"
import { Transaction } from "src/models/response/transaction.model"
import { axiosClient } from "./axios-client"



const PATH = {
    getAll: (userId: string) => `/get-transactions/${userId}`,
    create: '/add-transaction',
    update: '/update-transaction',
    remove: (transactionId: string) => `/delete-transaction/${transactionId}`
}




function create(data: TransactionCreate){
    return axiosClient<Transaction>({
        url: PATH.create,
        method: 'POST',
        data
    })
}

function getAll(userId: string){
    return axiosClient<Transaction[]>({
        url: PATH.getAll(userId),
        method: 'GET',
    })
}

function update(data: TransactionUpdate){
    return axiosClient<Transaction>({
        url: PATH.update,
        method: 'PUT',
        data
    })
}

function remove(transactionId: string){
    return axiosClient({
        url: PATH.remove(transactionId),
        method: 'DELETE'
    })
}

export const transactionApi = {
    create,
    getAll,
    update,
    remove
}

