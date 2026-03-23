import { CurrentTest } from "./dto/CurrentTest";
import { Question } from "./types/Question";

export class Converter {
    static Convert(test: CurrentTest) : Question {
        return {
            question: test.questionText,
            options: [test.questionAnswer1, test.questionAnswer2, test.questionAnswer3, test.questionAnswer4],
            correctAnswer: 0,
            weight: 0
        };
    }
}