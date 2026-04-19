import { AppStateDto } from "@/biz/dto/AppStateDto";
import { UserDto } from "@/biz/dto/UserDto";
import { Helper } from "@/biz/Helper";
import Http from "@/biz/Http";
import { mapTopic } from "@/biz/mappers/topicMapper";
import { NavItem } from "@/biz/models/NavItems";
import { Topic } from "@/biz/models/Topic";
import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";
import { FieldValues } from "react-hook-form";
import { Login } from "@/biz/models/Login";
import { Register } from "@/biz/models/Register";

export interface AppState {
    topics: Topic[];
    user: UserDto | null;
    forbiddenPages: NavItem[];
    loginFormFields: Login | null;
    registerFormFields: Register | null;
}

const initialState: AppState = {
    topics: [],
    user: null,
    forbiddenPages: [NavItem.Profile],
    loginFormFields: null,
    registerFormFields: null,
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
            state.forbiddenPages = [];
        },
        logOut: (state) => {
            state.user = null;
            localStorage.removeItem(Helper.UserKey);
            state.forbiddenPages = [NavItem.Profile];
        },
        setForbidenPages: (state, action: PayloadAction<NavItem[]>) => {
            state.forbiddenPages = action.payload;
        },

        // Login form reducers
        openLoginForm: (state) => {
            state.registerFormFields = null;
            state.loginFormFields = {
                username: "",
                password: "",
                errors: []
            };
        },
        closeLoginForm: (state) => {
            state.loginFormFields = null;
        },
        setLoginFormFields: (state, action: PayloadAction<Login | null>) => {
            state.loginFormFields = action.payload;
        },
        addLoginError: (state, action: PayloadAction<string>) => {
            if (state.loginFormFields === null) return;

            if (!state.loginFormFields.errors.includes(action.payload))
                state.loginFormFields.errors.push(action.payload);
        },

        // Register form reducers
        openRegisterForm: (state) => {
            state.loginFormFields = null;
            state.registerFormFields = {
                username: "",
                email: "",
                password: "",
                confirmPassword: "",
                errors: []
            };
        },
        closeRegisterForm: (state) => {
            state.registerFormFields = null;
        },
        setRegisterFormFields: (state, action: PayloadAction<Register | null>) => {
            state.registerFormFields = action.payload;
        },
        addRegisterError: (state, action: PayloadAction<string>) => {
            if (state.registerFormFields === null)
                return;

            if (!state.registerFormFields.errors.includes(action.payload))
                state.registerFormFields.errors.push(action.payload);
        },
        excludeRegistrationError: (state, action: PayloadAction<string>) => {
            if (state.registerFormFields === null)
                return;
            state.registerFormFields.errors =
            state.registerFormFields.errors.filter(error => error !== action.payload);
        },
    },
    extraReducers: (builder => {

        // initState
        builder.addCase(initState.fulfilled, (state, action) => {
            state.topics = action.payload.topics.map(mapTopic);
            state.user = action.payload.user;
            if (state.user) {
                localStorage.setItem(Helper.UserKey, JSON.stringify(state.user));
                state.forbiddenPages = [];
            } else {
                localStorage.removeItem(Helper.UserKey);
                state.forbiddenPages = [NavItem.Profile];
            }
        });
        builder.addCase(initState.rejected, (_, action) => {
            console.log("initState.rejected", action.payload);
        });

        // registerInUser
        builder.addCase(registerInUser.fulfilled, (state, action) => {
            state.user = action.payload;
            state.registerFormFields = null;
            if (state.user) {
                localStorage.setItem(Helper.UserKey, JSON.stringify(state.user));
                state.forbiddenPages = [];
            } else {
                localStorage.removeItem(Helper.UserKey);
                state.forbiddenPages = [NavItem.Profile];
            }
        });
        builder.addCase(registerInUser.rejected, (state, action) => {
            console.log("registerInUser.rejected", action.payload);
            if (state.registerFormFields === null)
                return;
            state.registerFormFields.errors = [];
            const messages = Object.values(action.payload?.error ?? {}).flat();
            state.registerFormFields.errors = messages;
        });

        // signInUser
        builder.addCase(signInUser.fulfilled, (state, action) => {
            state.user = action.payload;
            state.loginFormFields = null;
            if (state.user) {
                localStorage.setItem(Helper.UserKey, JSON.stringify(state.user));
                state.forbiddenPages = [];
            } else {
                localStorage.removeItem(Helper.UserKey);
                state.forbiddenPages = [NavItem.Profile];
            }
        });
        builder.addCase(signInUser.rejected, (state, action) => {
            console.log("signInUser.rejected", action.payload);
            state.loginFormFields!.errors = ['Invalid username or password.'];
        });
    })
});

export const {
    setUser,
    logOut,
    setForbidenPages,
    openLoginForm,
    closeLoginForm,
    setLoginFormFields,
    addLoginError,
    openRegisterForm,
    closeRegisterForm,
    setRegisterFormFields,
    addRegisterError,
    excludeRegistrationError } = appSlice.actions;