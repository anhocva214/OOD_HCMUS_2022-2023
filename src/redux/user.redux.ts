import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit'
import { RootState } from 'src/redux/reducer'
import { User } from 'src/models/response/user.model'
import { userApi } from '@apis/exports';
import { UserLogin, UserRegister } from 'src/models/request/user';


export const registerUser = createAsyncThunk(
    'users/register',
    async (data: UserRegister, { fulfillWithValue, rejectWithValue }) => {
        try {
            let response = await userApi.register(data)
            return fulfillWithValue(response)
        }
        catch (e) {
            return rejectWithValue(e)
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
        catch (e) {
            return rejectWithValue(e)
        }
    }
)


export interface UserState {
    loadingRegisterUser: boolean,
    loadingLoginUser: boolean,
    user: User,
}

export const initialState: UserState = {
    loadingRegisterUser: false,
    loadingLoginUser: false,
    user: null,
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
    },
})

export const userReducer = userSlice.reducer
export const userSelector = (state: RootState) => state.user

