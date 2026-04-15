import { StatisticsPageDto } from "@/biz/dto/StatisticsPageDto";
import { StatisticsRequestDto } from "@/biz/dto/StatisticsRequestDto";
import Http from "@/biz/Http";
import { Row } from "@/biz/models/Row";
import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";

export interface TopStatisticsState {
    rows: Row[];
    pageSize: number;
    pageNumber: number;
    scoreThreshold: number;
    topicId: number;
    totalCount: number;
}

const initialState: TopStatisticsState = {
    rows: [],
    pageSize: 10,
    pageNumber: 1,
    scoreThreshold: 0,
    topicId: 0,
    totalCount: 0,
};

export const getPage = createAsyncThunk<StatisticsPageDto>(
    'stat/getPage',
    async (_, thunkAPI) => {
        try {
            const state = thunkAPI.getState() as { statState: TopStatisticsState };

            return await Http.Statistics.page({
                pageNumber: state.statState.pageNumber - 1,
                pageSize: state.statState.pageSize,
                scoreThreshold: state.statState.scoreThreshold,
                topicId: state.statState.topicId,
            });
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.status });
        }
    }
);

export const topStatisticsSlice = createSlice({
    name: 'statState',
    initialState,
    reducers: {
        setFilter: (state, action: PayloadAction<StatisticsRequestDto>) => {
            state.pageSize = action.payload.pageSize;
            state.pageNumber = action.payload.pageNumber;
            state.scoreThreshold = action.payload.scoreThreshold;
            state.topicId = action.payload.topicId;
        },
        setTopic: (state, action: PayloadAction<number>) => {
            state.topicId = action.payload;
            state.pageNumber = 1;
        },
        setScore: (state, action: PayloadAction<number>) => {
            state.scoreThreshold = action.payload;
            state.pageNumber = 1;
        },
        setPageSize: (state, action: PayloadAction<number>) => {
            state.pageSize = action.payload;
            state.pageNumber = 1;
        },
        setPageNumber: (state, action: PayloadAction<number>) => {
            state.pageNumber = action.payload;
        },
    },
    extraReducers: (builder => {

        // getPage
        builder.addCase(getPage.fulfilled, (state, action) => {
            state.totalCount = action.payload.totalCount;
            state.rows = action.payload.rows.map(rowDto => ({
                ...rowDto,
            }));
        });
        builder.addCase(getPage.rejected, (_, action) => {
            console.log("getPage.rejected" + action.payload);
        });
    })
});

export const { setFilter, setTopic, setScore, setPageSize, setPageNumber } = topStatisticsSlice.actions;