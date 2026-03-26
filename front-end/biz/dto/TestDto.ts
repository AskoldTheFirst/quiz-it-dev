import { QuestionDto } from "./QuestionDto";

export interface TestDto {
    testId: number;
    technologyName: string;
    secondsLeft: number;
    questionCount: number;
    question: QuestionDto;
}