import { TestQuestion } from "./TestQuestion";

export interface TestCreated {
    testId: number;
    totalQuestions: number;
    secondsLeft: number;
    technologyName: string;
    testColor: string;
    firstQuestion: TestQuestion;
}