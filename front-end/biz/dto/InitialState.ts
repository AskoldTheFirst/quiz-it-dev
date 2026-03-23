import { CurrentTest } from "./CurrentTest";
import { TechnologyDto } from "./TechnologyDto";
import { userDto } from "./userDto";

export interface InitialState {
    technologies: TechnologyDto[];
    user: userDto | null;
    currentTest: CurrentTest | null;
}