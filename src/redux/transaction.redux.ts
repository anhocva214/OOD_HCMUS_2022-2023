import { transactionApi } from '@apis/transaction'
import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit'
import { TransactionCreate, TransactionUpdate } from 'src/models/request/transaction.model'
import { Transaction } from 'src/models/response/transaction.model'
import { RootState } from 'src/redux/reducer'


export const createTransaction = createAsyncThunk(
    'transaction/create',
    async (data: TransactionCreate, { fulfillWithValue, rejectWithValue }) => {
        try {
            let res = await transactionApi.create(data)
            return fulfillWithValue(new Transaction(res))
        }
        catch (err) {
            return rejectWithValue(err)
        }
    }
)

export const getAllTransactions = createAsyncThunk(
    'transaction/get-all',
    async (data: undefined, { fulfillWithValue, rejectWithValue, getState }) => {
        try {
            let { user } = getState() as RootState
            let res = await transactionApi.getAll(user.user.id)
            return fulfillWithValue(res.map(item => new Transaction(item)))
        }
        catch (err) {
            return rejectWithValue(err)
        }
    }
)

export const updateTransaction = createAsyncThunk(
    'transaction/update',
    async (data: TransactionUpdate, { fulfillWithValue, rejectWithValue}) => {
        try {
            let res = await transactionApi.update(data)
            return fulfillWithValue(res)
        }
        catch (err) {
            return rejectWithValue(err)
        }
    }
)

export const removeTransaction = createAsyncThunk(
    'transaction/remove',
    async (transactionId: string, { fulfillWithValue, rejectWithValue }) => {
        try {
            await transactionApi.remove(transactionId)
            return fulfillWithValue(transactionId)
        }
        catch (err) {
            return rejectWithValue(err)
        }
    }
)


export interface TransactionState {
    transactions: Transaction[],
    transactionsSpending: Transaction[],
    transactionsIncome: Transaction[],
    loadingTransactions: boolean,
    loadingCreateTransaction: boolean,
    loadingUpdateTransaction: boolean,
    loadingRemoveTransaction: boolean
}

export const initialState: TransactionState = {
    transactions: [],
    transactionsIncome: [],
    transactionsSpending: [],
    loadingCreateTransaction: false,
    loadingTransactions: false,
    loadingUpdateTransaction: false,
    loadingRemoveTransaction: false
}

export const slice = createSlice({
    name: 'transaction',
    initialState,
    reducers: {
        classifyTransactions: (state, {payload}: PayloadAction<{categoryIncomeId: string}>)=>{
            state.transactionsIncome = state.transactions.filter(item => item.categoryId == payload.categoryIncomeId)
            state.transactionsSpending = state.transactions.filter(item => item.categoryId != payload.categoryIncomeId)
        }
    },
    extraReducers: (builder) => {
        builder
            .addCase(createTransaction.pending, (state) => {
                state.loadingCreateTransaction = true;
            })
            .addCase(createTransaction.fulfilled, (state, { payload }) => {
                state.loadingCreateTransaction = false;
                state.transactions.push(payload)
            })
            .addCase(createTransaction.rejected, (state) => {
                state.loadingCreateTransaction = false;
            })

        builder
            .addCase(getAllTransactions.pending, (state) => {
                state.loadingTransactions = true;
            })
            .addCase(getAllTransactions.fulfilled, (state, { payload }) => {
                state.loadingTransactions = false;
                state.transactions = payload
            })
            .addCase(getAllTransactions.rejected, (state) => {
                state.loadingTransactions = false;
                state.transactions = []
            })

        builder
            .addCase(updateTransaction.pending, (state) => {
                state.loadingUpdateTransaction = true;
            })
            .addCase(updateTransaction.fulfilled, (state, { payload }) => {
                state.loadingUpdateTransaction = false;
                state.transactions = state.transactions.map(item => {
                    if (item.id == payload.id) return payload
                    else return item
                })
            })
            .addCase(updateTransaction.rejected, (state) => {
                state.loadingUpdateTransaction = false;
            })

        builder
            .addCase(removeTransaction.pending, (state) => {
                state.loadingRemoveTransaction = true;
            })
            .addCase(removeTransaction.fulfilled, (state, { payload }) => {
                state.loadingRemoveTransaction = false;
                state.transactions = state.transactions.filter(item => item.id != payload)
            })
            .addCase(removeTransaction.rejected, (state) => {
                state.loadingRemoveTransaction = false;
            })
    },
})

export const transactionReducer = slice.reducer
export const transactionSelector = (state: RootState) => state.transaction


export const classifyTransactions = slice.actions.classifyTransactions
