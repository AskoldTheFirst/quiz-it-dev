import { ProfileDto } from "@/biz/dto/profile/ProfileDto";
import Http from "@/biz/Http";
import { Profile } from "@/biz/models/profile/Profile";
import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";

export interface ProfileState {
    profile: Profile | null;
    isLoaded: boolean;
}

const initialState: ProfileState = {
    profile: null,
    isLoaded: false,
};

export const getProfile = createAsyncThunk<ProfileDto>(
    'profile/getProfile',
    async (_, thunkAPI) => {
        try {
            return await Http.Statistics.profile();
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.status });
        }
    }
);

export const hide = createAsyncThunk<ProfileDto>(
    'profile/hide',
    async (_, thunkAPI) => {
        try {
            return await Http.Statistics.hide();
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.status });
        }
    }
);

export const profileSlice = createSlice({
    name: 'profile',
    initialState,
    reducers: {},
    extraReducers: (builder => {

        // getProfile
        builder.addCase(getProfile.fulfilled, (state, action) => {
            state.profile = { ...action.payload };
            state.isLoaded = true;
        });
        builder.addCase(getProfile.rejected, (state, action) => {
            state.isLoaded = true;
            console.log("getProfile.rejected", action.payload);
        });

        // hide
        builder.addCase(hide.fulfilled, (state, action) => {
            state.profile = { ...action.payload };
        });
        builder.addCase(hide.rejected, (_, action) => {
            console.log("hide.rejected", action.payload);
        });
    })
});