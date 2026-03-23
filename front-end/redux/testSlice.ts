import { CurrentTest } from "@/biz/dto/CurrentTest";
import { TestCreated } from "@/biz/dto/TestCreated";
import { TestQuestion } from "@/biz/dto/TestQuestion";
import Http from "@/biz/Http";
import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";

export interface TestState {
    test: CurrentTest | null;
}

const initialState: TestState = {
    test: null,
};

export const current = createAsyncThunk<CurrentTest>(
    'test/current',
    async (data, thunkAPI) => {
        try {
            return await Http.Test.current();
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.status });
        }
    });

export const createTest = createAsyncThunk<TestCreated, string>(
    'test/createTest',
    async (technologyName, thunkAPI) => {
        try {
            return await Http.Test.create(encodeURIComponent(technologyName));
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.status });
        }
    });

export const nextQuestion = createAsyncThunk<TestQuestion, number>(
    'test/nextQuestion',
    async (data, thunkAPI) => {
        try {
            return await Http.Test.nextQuestion(data);
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.status });
        }
    });

export const testSlice = createSlice({
    name: 'test',
    initialState,
    reducers: {
        setTest: (state, action) => {
            state.test = action.payload;
        },
    },
    extraReducers: (builder => {
        builder.addCase(current.fulfilled, (state, action) => {
            state.test = action.payload;
            // go to quiz page -> quiz/[topicId]
        });
        builder.addCase(current.rejected, (_state, action) => {
            console.log("current.rejected" + action.payload);
        });

        builder.addCase(createTest.fulfilled, (state, action) => {
            let ap = action.payload;
            state.test = {
                testName: ap.technologyName,
                testColor: ap.testColor,
                testId: ap.testId,
                totalQuestions: ap.totalQuestions,
                spentTimeInSeconds: ap.secondsLeft,
                number: 1,
                questionId: ap.firstQuestion.questionId,
                testQuestionId: ap.firstQuestion.testQuestionId,
                questionText: ap.firstQuestion.text,
                questionAnswer1: ap.firstQuestion.answer1,
                questionAnswer2: ap.firstQuestion.answer2,
                questionAnswer3: ap.firstQuestion.answer3,
                questionAnswer4: ap.firstQuestion.answer4,
            };
        });
        builder.addCase(createTest.rejected, (_state, action) => {
            console.log("createTest.rejected" + action.payload);
        });

        builder.addCase(nextQuestion.fulfilled, (state, action) => {

            if (state.test) {

                let st = state.test;
                let ap = action.payload;
                
                st.number = state.test.number + 1;
                st.questionId = ap.questionId;
                st.testQuestionId = ap.testQuestionId;
                st.questionText = ap.text;
                st.questionAnswer1 = ap.answer1;
                st.questionAnswer2 = ap.answer2;
                st.questionAnswer3 = ap.answer3;
                st.questionAnswer4 = ap.answer4;
            }
        });
        builder.addCase(nextQuestion.rejected, (_state, action) => {
            console.log("nextQuestion.rejected" + action.payload);
        });
    })
});