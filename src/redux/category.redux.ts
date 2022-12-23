import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit'
import { RootState } from 'src/redux/reducer'
import { User } from 'src/models/response/user.model'
import { UserForgotPassword, UserLogin, UserRegister } from 'src/models/request/user';
import { categoryApi } from '@apis/category.api';
import { Category } from 'src/models/response/category.model';

export const CATEGORIES = ["Sức khoẻ", "Chuyển tiền", "Ăn uống", "Mua sắm", "Giáo dục", "Khác", "Thu Nhập"] as const


export const getAllCategories = createAsyncThunk(
    'users/register',
    async (data: undefined, { fulfillWithValue, rejectWithValue }) => {
        try {
            let data = await categoryApi.getAllCategories()
            if (data.length === 0) {
                await Promise.all(CATEGORIES.map(async (item) => {
                    await categoryApi.createCategory(new Category({ id: '', name: item }))
                }))
                data = await categoryApi.getAllCategories()
            }
            return fulfillWithValue(data)
        }
        catch (err) {
            return rejectWithValue(err)
        }
    }
)


export interface CategoryState {
    categories: Category[],
    loadingAllCategories: boolean
}

export const initialState: CategoryState = {
    categories: [],
    loadingAllCategories: false
}

export const slice = createSlice({
    name: 'category',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(getAllCategories.pending, (state) => {
                state.loadingAllCategories = true;
            })
            .addCase(getAllCategories.fulfilled, (state, { payload }) => {
                state.loadingAllCategories = false;
                state.categories = payload
            })
            .addCase(getAllCategories.rejected, (state) => {
                state.loadingAllCategories = false;
                state.categories = []
            })


    },
})

export const categoryReducer = slice.reducer
export const categorySelector = (state: RootState) => state.category

