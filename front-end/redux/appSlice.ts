import { appStateDto } from "@/biz/dto/appStateDto";
import { TechnologyDto } from "@/biz/dto/TechnologyDto";
import { userDto } from "@/biz/dto/userDto";
import { Helper } from "@/biz/Helper";
import Http from "@/biz/Http";
import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { FieldValues } from "react-hook-form";

export interface AppState {
    technologies: TechnologyDto[];
    user: userDto | null;
}

const initialState: AppState = {
    technologies: [],
    user: null,
};

export const initState = createAsyncThunk<appStateDto>(
    'appState/initState',
    async (data, thunkAPI) => {

        let token = localStorage.getItem(Helper.UserKey);
        if (token) {
            let json = JSON.parse(token);
            thunkAPI.dispatch(setUser({ user: json })); // Set user from localStorage before API call
        }

        try {
            return await Http.App.initState();
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.status });
        }
    }
);

export const registerInUser = createAsyncThunk<userDto, FieldValues>(
    'appState/registerInUser',
    async (data, thunkAPI) => {
        try {
            return await Http.App.register(data);
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.status });
        }
    }
);

export const signInUser = createAsyncThunk<userDto, FieldValues>(
    'appState/signInUser',
    async (data, thunkAPI) => {
        try {
            return await Http.App.login(data);
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.status });
        }
    }
);

export const appSlice = createSlice({
    name: 'appState',
    initialState,
    reducers: {
        setUser: (state, action) => {
            state.user = action.payload.user;
            localStorage.setItem(Helper.UserKey, JSON.stringify(state.user));
        },
        signOut: (state) => {
            state.user = null;
            localStorage.removeItem(Helper.UserKey);
        },
        setState: (state, action) => {
            state.technologies = action.payload.technologies;
            state.user = action.payload.user;
            localStorage.setItem(Helper.UserKey, JSON.stringify(state.user));
        }
    },
    extraReducers: (builder => {

        // initState
        builder.addCase(initState.fulfilled, (state, action) => {
            state.technologies = action.payload.technologies;
            state.user = action.payload.user;
            if (state.user) {
                localStorage.setItem(Helper.UserKey, JSON.stringify(state.user));
            } else {
                localStorage.removeItem(Helper.UserKey);
            }
        });
        builder.addCase(initState.rejected, (_, action) => {
            console.log("initState.rejected" + action.payload);
        });

        // registerInUser
        builder.addCase(registerInUser.fulfilled, (state, action) => {
            state.user = action.payload;
            if (state.user) {
                localStorage.setItem(Helper.UserKey, JSON.stringify(state.user));
            } else {
                localStorage.removeItem(Helper.UserKey);
            }
        });
        builder.addCase(registerInUser.rejected, (_state, action) => {
            console.log("registerInUser.rejected" + action.payload);
        });

        // signInUser
        builder.addCase(signInUser.fulfilled, (state, action) => {
            state.user = action.payload;
            if (state.user) {
                localStorage.setItem(Helper.UserKey, JSON.stringify(state.user));
            } else {
                localStorage.removeItem(Helper.UserKey);
            }
        });
        builder.addCase(signInUser.rejected, (_state, action) => {
            console.log("signInUser.rejected" + action.payload);
        });
    })
});

export const { setUser, signOut } = appSlice.actions;