import { combineReducers } from '@reduxjs/toolkit'
import { categoryReducer } from './category.redux'
import { transactionReducer } from './transaction.redux'

/* PLOP_INJECT_IMPORT */
import { userReducer } from './user.redux'


const rootReducer = combineReducers({
    /* PLOP_INJECT_USE */
    user: userReducer,
    category: categoryReducer,
    transaction: transactionReducer
})

export type RootState = ReturnType<typeof rootReducer>

export default rootReducer