import { Answer } from "./Answer";

export interface TestResult {
    technologyName: string;
    finalScore: number;
    totalPoints: number;
    earnedPoints: number;
    answeredCount: number;
    answers: Answer[];
}