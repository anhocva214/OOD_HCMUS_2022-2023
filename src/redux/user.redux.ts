import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit'
import { RootState } from 'src/redux/reducer'
import { User } from 'src/models/response/user.model'
import { userApi } from '@apis/exports';
import { UserForgotPassword, UserLogin, UserRegister } from 'src/models/request/user';


export const registerUser = createAsyncThunk(
    'users/register',
    async (data: UserRegister, { fulfillWithValue, rejectWithValue }) => {
        try {
            let response = await userApi.register(data)
            return fulfillWithValue(response)
        }
        catch (err) {
            return rejectWithValue(err)
        }
    }
)

export const loginUser = createAsyncThunk(
    'users/login',
    async (data: UserLogin, { fulfillWithValue, rejectWithValue }) => {
        try {
            let response = await userApi.login(data)
            return fulfillWithValue(response)
        }
        catch (err) {
            return rejectWithValue(err)
        }
    }
)

export const forgotPasswordUser = createAsyncThunk(
    'users/forgotPassword',
    async (data: UserForgotPassword, { fulfillWithValue, rejectWithValue }) => {
        try {
            await userApi.forgotPassword(data)
            fulfillWithValue(null)
        }
        catch (err) {
            return rejectWithValue(err)
        }
    }
)

export interface UserState {
    loadingRegisterUser: boolean,
    loadingLoginUser: boolean,
    user: User,
    loadingForgotPasswordUser: boolean
}

export const initialState: UserState = {
    loadingRegisterUser: false,
    loadingLoginUser: false,
    user: null,
    loadingForgotPasswordUser: false
}

export const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(registerUser.pending, (state) => {
                state.loadingRegisterUser = true;
            })
            .addCase(registerUser.fulfilled, (state) => {
                state.loadingRegisterUser = false;
            })
            .addCase(registerUser.rejected, (state) => {
                state.loadingRegisterUser = false;
            })

        builder
            .addCase(loginUser.pending, (state) => {
                state.loadingLoginUser = true;
            })
            .addCase(loginUser.fulfilled, (state, { payload }: PayloadAction<User>) => {
                state.loadingLoginUser = false;
                state.user = payload;
            })
            .addCase(loginUser.rejected, (state) => {
                state.loadingLoginUser = false
            })

        builder
            .addCase(forgotPasswordUser.pending, (state) => {
                state.loadingForgotPasswordUser = true;
            })
            .addCase(forgotPasswordUser.fulfilled, (state) => {
                state.loadingForgotPasswordUser = false
            })
            .addCase(forgotPasswordUser.rejected, (state) => {
                state.loadingForgotPasswordUser = false
            })
    },
})

export const userReducer = userSlice.reducer
export const userSelector = (state: RootState) => state.user

