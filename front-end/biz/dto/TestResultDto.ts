import { AnswerDto } from "./AnswerDto";

export interface TestResultDto {
    technologyName: string;
    finalScore: number;
    totalPoints: number;
    earnedPoints: number;
    answeredCount: number;
    answers: AnswerDto[];
}