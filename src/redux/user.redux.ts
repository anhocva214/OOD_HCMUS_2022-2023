import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit'
import { RootState } from 'src/redux/reducer'
import { User, UserUpdate } from 'src/models/response/user.model'
import { UserForgotPassword, UserLogin, UserRegister } from 'src/models/request/user';
import { userApi } from '@apis/user.api';
import cookie from 'react-cookies'

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

export const getUserById = createAsyncThunk(
    'users/me',
    async (data: null, { fulfillWithValue, rejectWithValue }) => {
        try {
            let userId = cookie.load('userId')
            let user = await userApi.getUserById(userId)
            return fulfillWithValue(user)
        }
        catch (err) {
            return rejectWithValue(err)
        }
    }
)

export const updateUser = createAsyncThunk(
    'users/update',
    async (data: UserUpdate, { fulfillWithValue, rejectWithValue }) => {
        try {
            let user = await userApi.updateUser(data)
            return fulfillWithValue(user)
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
    loadingForgotPasswordUser: boolean,
    loadingUpdateUser: boolean,
}

export const initialState: UserState = {
    loadingRegisterUser: false,
    loadingLoginUser: false,
    user: null,
    loadingForgotPasswordUser: false,
    loadingUpdateUser: false
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

        builder
            .addCase(getUserById.pending, (state) => {
            })
            .addCase(getUserById.fulfilled, (state, { payload }) => {
                state.user = payload;
            })
            .addCase(getUserById.rejected, (state) => {
            })

        builder
            .addCase(updateUser.pending, (state) => {
                state.loadingUpdateUser = true;
            })
            .addCase(updateUser.fulfilled, (state, { payload }) => {
                state.loadingUpdateUser = false;
                state.user = payload;
            })
            .addCase(updateUser.rejected, (state) => {
                state.loadingUpdateUser = false;
            })
    },
})

export const userReducer = userSlice.reducer
export const userSelector = (state: RootState) => state.user

