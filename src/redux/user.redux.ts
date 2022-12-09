import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit'
import { RootState } from 'src/redux/reducer'
import { User } from 'src/models/response/user.model'
import { userApi } from '@apis/exports';
import { UserRegister } from 'src/models/request/user';


export const registerUser = createAsyncThunk(
    'users/register',
    async (data: UserRegister, {fulfillWithValue, rejectWithValue}) => {
        try{
            let response = await userApi.register(data)
            return fulfillWithValue(response)
        }
        catch(e){
            return rejectWithValue(e)
        }
    }
)


export interface UserState {
    loadingRegisterUser: boolean,
}

export const initialState: UserState = {
    loadingRegisterUser: false,
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
            .addCase(registerUser.rejected, (state, {payload}: PayloadAction<any>) => {
                state.loadingRegisterUser = false;
            })
    },
})

export const userReducer = userSlice.reducer
export const userSelector = (state: RootState) => state.user

