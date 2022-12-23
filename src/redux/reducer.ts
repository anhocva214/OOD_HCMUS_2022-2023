import { combineReducers } from '@reduxjs/toolkit'
import { categoryReducer } from './category.redux'

/* PLOP_INJECT_IMPORT */
import { userReducer } from './user.redux'


const rootReducer = combineReducers({
    /* PLOP_INJECT_USE */
    user: userReducer,
    category: categoryReducer
})

export type RootState = ReturnType<typeof rootReducer>

export default rootReducer