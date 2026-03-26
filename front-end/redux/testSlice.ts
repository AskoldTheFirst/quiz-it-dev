import { AnswerRequestDto } from "@/biz/dto/AnswerRequestDto";
import { AnswerResponseDto } from "@/biz/dto/AnswerResponseDto";
import { TestDto } from "@/biz/dto/TestDto";
import Http from "@/biz/Http";
import { mapAnswer } from "@/biz/mappers/answerMapper";
import { mapCurrentTest } from "@/biz/mappers/currentTestMapper";
import { CurrentTest } from "@/biz/models/CurrentTest";
import { TestResult } from "@/biz/models/TestResult";
import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";

export interface TestState {
    test: CurrentTest | null;
    result: TestResult | null;
}

const initialState: TestState = {
    test: null,
    result: null,
};

export const current = createAsyncThunk<TestDto>(
    'test/current',
    async (data, thunkAPI) => {
        try {
            return await Http.Test.current();
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.status });
        }
    });

export const createTest = createAsyncThunk<TestDto, string>(
    'test/createTest',
    async (technologyName, thunkAPI) => {
        try {
            return await Http.Test.create(encodeURIComponent(technologyName));
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.status });
        }
    });

export const cancelTest = createAsyncThunk<void, number>(
    'test/cancelTest',
    async (testId, thunkAPI) => {
        try {
            return await Http.Test.cancel(testId);
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.status });
        }
    });

export const answer = createAsyncThunk<AnswerResponseDto, AnswerRequestDto>(
    'test/answer',
    async (param, thunkAPI) => {
        try {
            return await Http.Test.answer(param);
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.status });
        }
    });

export const testSlice = createSlice({
    name: 'test',
    initialState,
    reducers: {
        setTestData: (state, action: PayloadAction<{ test: CurrentTest | null; result: TestResult | null }>) => {
            state.test = action.payload.test;
            state.result = action.payload.result;
        },
    },
    extraReducers: (builder => {

        // current:
        builder.addCase(current.fulfilled, (state, action) => {
            state.test = mapCurrentTest(action.payload);
            state.result = null;
        });
        builder.addCase(current.rejected, (_state, action) => {
            console.log("current.rejected" + action.payload);
        });

        // createTest:
        builder.addCase(createTest.fulfilled, (state, action) => {
            state.test = mapCurrentTest(action.payload);
            state.result = null;
        });
        builder.addCase(createTest.rejected, (_state, action) => {
            console.log("createTest.rejected" + action.payload);
        });

        // cancelTest:
        builder.addCase(cancelTest.fulfilled, (state, action) => {
            state.test = null;
            state.result = null;
        });
        builder.addCase(cancelTest.rejected, (_state, action) => {
            console.log("cancelTest.rejected" + action.payload);
        });

        // answer:
        builder.addCase(answer.fulfilled, (state, action) => {
            let ap = action.payload;

            if (ap.nextQuestion && state.test) {
                state.test.questionText = ap.nextQuestion.text;
                state.test.questionAnswer1 = ap.nextQuestion.answer1;
                state.test.questionAnswer2 = ap.nextQuestion.answer2;
                state.test.questionAnswer3 = ap.nextQuestion.answer3;
                state.test.questionAnswer4 = ap.nextQuestion.answer4;
                state.test.questionId = ap.nextQuestion.questionId;
                state.test.testQuestionId = ap.nextQuestion.testQuestionId;
                state.test.number = ap.nextQuestion.number;

                state.result = null;
            }
            else if (ap.testResult) {
                state.result = {
                    technologyName: ap.testResult.technologyName,
                    answeredCount: ap.testResult.answeredCount,
                    finalScore: ap.testResult.finalScore,
                    earnedPoints: ap.testResult.earnedPoints,
                    totalPoints: ap.testResult.totalPoints,
                    answers: ap.testResult.answers.map(a => mapAnswer(a)),
                };

                state.test = null;
            }
        });
        builder.addCase(answer.rejected, (state, action) => {
            console.log("answer.rejected" + action.payload);
        });
    })
});

export const { setTestData } = testSlice.actions;