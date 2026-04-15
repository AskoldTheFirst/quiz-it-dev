import { configureStore } from '@reduxjs/toolkit';
import { TypedUseSelectorHook, useDispatch, useSelector } from "react-redux";
import { appSlice } from './appSlice';
import { testSlice } from './testSlice';
import { statSlice } from './statSlice';
import { mistakesSlice } from './mistakesSlice';

export const store = configureStore({
  reducer: {
    appState: appSlice.reducer,
    testState: testSlice.reducer,
    statState: statSlice.reducer,
    mistakesState: mistakesSlice.reducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export const useAppDispatch = () => useDispatch<AppDispatch>();
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;