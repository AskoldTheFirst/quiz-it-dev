import { MistakeDto } from "@/biz/dto/MistakeDto";
import { MistakesRequestDto } from "@/biz/dto/MistakesRequestDto";
import Http from "@/biz/Http";
import { mapMistakes } from "@/biz/mappers/mistakesMapper";
import { Mistake } from "@/biz/models/Mistake";
import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";

export interface MistakesState {
    mistakes: Mistake[];
    topicId: number;
    isByTotal: boolean;
    isLoaded: boolean;
}

const initialState: MistakesState = {
    mistakes: [],
    topicId: 0,
    isByTotal: true,
    isLoaded: false,
};

export const loadMistakes = createAsyncThunk<MistakeDto[], MistakesRequestDto>(
    'MistakesState/loadMistakes',
    async (data, thunkAPI) => {
        try {
            return await Http.Statistics.mistakes(data);
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.status });
        }
    }
);

export const mistakesSlice = createSlice({
    name: 'MistakesState',
    initialState,
    reducers: {
        setTopicId: (state, action) => {
            state.topicId = action.payload;
        },
        setIsByTotal: (state, action) => {
            state.isByTotal = action.payload;
        },
    },
    extraReducers: (builder => {

        // mistakes
        builder.addCase(loadMistakes.fulfilled, (state, action) => {
            state.mistakes = mapMistakes(action.payload);
            state.isLoaded = true;
        });
        builder.addCase(loadMistakes.rejected, (state, action) => {
            state.isLoaded = true;
            console.log("loadMistakes.rejected", action.payload);
        });
    })
});

export const { setTopicId, setIsByTotal } = mistakesSlice.actions;