import { AppStateDto } from "@/biz/dto/AppStateDto";
import { UserDto } from "@/biz/dto/UserDto";
import { Helper } from "@/biz/Helper";
import Http from "@/biz/Http";
import { mapTopic } from "@/biz/mappers/topicMapper";
import { NavItem } from "@/biz/models/NavItems";
import { Topic } from "@/biz/models/Topic";
import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";
import { FieldValues } from "react-hook-form";
import { AuthState } from "@/biz/models/AuthState";

export interface AppState {
    topics: Topic[];
    user: UserDto | null;
    forbidenPages: NavItem[];
    currentPage: NavItem;
    authState: AuthState;
}

const initialAuthState: AuthState = {
    username: "",
    email: "",
    password: "",
    confirmPassword: "",
    errors: []
};

const initialState: AppState = {
    topics: [],
    user: null,
    forbidenPages: [NavItem.Profile],
    currentPage: NavItem.Quiz,
    authState: initialAuthState,
};

type RegisterErrorPayload = {
    error: Record<string, string[]>;
};

export const initState = createAsyncThunk<AppStateDto>(
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
            return await Http.App.initState();
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.status });
        }
    }
);

export const registerInUser = createAsyncThunk<UserDto, FieldValues, { rejectValue: RegisterErrorPayload }>(
    'appState/registerInUser',
    async (data, thunkAPI) => {
        try {
            return await Http.App.register(data);
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.errors });
        }
    }
);

export const signInUser = createAsyncThunk<UserDto, FieldValues, { rejectValue: RegisterErrorPayload }>(
    'appState/signInUser',
    async (data, thunkAPI) => {
        try {
            return await Http.App.login(data);
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.errors });
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
        },
        cleanAuthState: (state) => {
            state.authState = initialAuthState;
        },
        addError: (state, action: PayloadAction<string>) => {
            if (!state.authState.errors.includes(action.payload))
                state.authState.errors.push(action.payload);
        },
        excludeError: (state, action: PayloadAction<string>) => {
            state.authState.errors = state.authState.errors.filter(error => error !== action.payload);
        },
        setAuthFormFields: (state, action: PayloadAction<AuthState>) => {
            state.authState = action.payload;
        }
    },
    extraReducers: (builder => {

        // initState
        builder.addCase(initState.fulfilled, (state, action) => {
            state.topics = action.payload.topics.map(mapTopic);
            state.user = action.payload.user;
            state.currentPage = NavItem.Quiz;
            state.authState.errors = [];
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
            state.authState = initialAuthState;
            if (state.user) {
                localStorage.setItem(Helper.UserKey, JSON.stringify(state.user));
                state.forbidenPages = [];
            } else {
                localStorage.removeItem(Helper.UserKey);
                state.forbidenPages = [NavItem.Profile];
            }
        });
        builder.addCase(registerInUser.rejected, (state, action) => {
            console.log("registerInUser.rejected", action.payload);
            state.authState.errors = [];
            const messages = Object.values(action.payload?.error ?? {}).flat();
            state.authState.errors = messages;
        });

        // signInUser
        builder.addCase(signInUser.fulfilled, (state, action) => {
            state.user = action.payload;
            state.authState = initialAuthState;
            if (state.user) {
                localStorage.setItem(Helper.UserKey, JSON.stringify(state.user));
                state.forbidenPages = [];
            } else {
                localStorage.removeItem(Helper.UserKey);
                state.forbidenPages = [NavItem.Profile];
            }
        });
        builder.addCase(signInUser.rejected, (state, action) => {
            console.log("signInUser.rejected", action.payload);
            state.authState.errors = ['Invalid username or password.'];
        });
    })
});

export const { setUser, logOut, setCurrentPage, setForbidenPages, cleanAuthState, addError, excludeError, setAuthFormFields } = appSlice.actions;