import { AppStateDto } from "@/biz/dto/AppStateDto";
import { UserDto } from "@/biz/dto/UserDto";
import { Helper } from "@/biz/Helper";
import Http from "@/biz/Http";
import { mapTopic } from "@/biz/mappers/topicMapper";
import { NavItem } from "@/biz/models/NavItems";
import { Topic } from "@/biz/models/Topic";
import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";
import { FieldValues } from "react-hook-form";

export interface AppState {
    topics: Topic[];
    user: UserDto | null;
    forbidenPages: NavItem[];
    currentPage: NavItem;
}

const initialState: AppState = {
    topics: [],
    user: null,
    forbidenPages: [NavItem.Profile],
    currentPage: NavItem.Quiz,
};

export const initState = createAsyncThunk<AppState>(
    'appState/initState',
    async (_, thunkAPI) => {
        
        let token = localStorage.getItem(Helper.UserKey);
        if (token) {
            try {
                let json = JSON.parse(token);
                thunkAPI.dispatch(setUser({ user: json })); // Set user from localStorage before API call
            }
            catch {
                localStorage.removeItem(Helper.UserKey);
            }
        }

        try {
            const response = await Http.App.initState() as AppStateDto;
            return {
                user: response.user,
                topics: response.topics.map(mapTopic),
                forbidenPages: response.user == null ? [NavItem.Profile] : [],
                currentPage: NavItem.Quiz
            }
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.status });
        }
    }
);

export const registerInUser = createAsyncThunk<UserDto, FieldValues>(
    'appState/registerInUser',
    async (data, thunkAPI) => {
        try {
            return await Http.App.register(data);
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.status });
        }
    }
);

export const signInUser = createAsyncThunk<UserDto, FieldValues>(
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
            state.forbidenPages = [];
        },
        logOut: (state) => {
            state.user = null;
            localStorage.removeItem(Helper.UserKey);
            state.forbidenPages = [NavItem.Profile];
        },
        setCurrentPage: (state, action: PayloadAction<NavItem>) => {
            state.currentPage = action.payload;
        },
        setForbidenPages: (state, action: PayloadAction<NavItem[]>) => {
            state.forbidenPages = action.payload;
        }
    },
    extraReducers: (builder => {

        // initState
        builder.addCase(initState.fulfilled, (state, action) => {
            state.topics = action.payload.topics;
            state.user = action.payload.user;
            if (state.user) {
                localStorage.setItem(Helper.UserKey, JSON.stringify(state.user));
                state.forbidenPages = [];
            } else {
                localStorage.removeItem(Helper.UserKey);
                state.forbidenPages = [NavItem.Profile];
            }
        });
        builder.addCase(initState.rejected, (_, action) => {
            console.log("initState.rejected", action.payload);
        });

        // registerInUser
        builder.addCase(registerInUser.fulfilled, (state, action) => {
            state.user = action.payload;
            if (state.user) {
                localStorage.setItem(Helper.UserKey, JSON.stringify(state.user));
                state.forbidenPages = [];
            } else {
                localStorage.removeItem(Helper.UserKey);
                state.forbidenPages = [NavItem.Profile];
            }
        });
        builder.addCase(registerInUser.rejected, (_state, action) => {
            console.log("registerInUser.rejected", action.payload);
        });

        // signInUser
        builder.addCase(signInUser.fulfilled, (state, action) => {
            state.user = action.payload;
            if (state.user) {
                localStorage.setItem(Helper.UserKey, JSON.stringify(state.user));
                state.forbidenPages = [];
            } else {
                localStorage.removeItem(Helper.UserKey);
                state.forbidenPages = [NavItem.Profile];
            }
        });
        builder.addCase(signInUser.rejected, (_state, action) => {
            console.log("signInUser.rejected", action.payload);
        });
    })
});

export const { setUser, logOut, setCurrentPage, setForbidenPages } = appSlice.actions;