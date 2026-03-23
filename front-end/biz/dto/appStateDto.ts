import { CurrentTest } from "./CurrentTest";
import { TechnologyDto } from "./TechnologyDto";
import { userDto } from "./userDto";

export interface appStateDto {
    technologies: TechnologyDto[];
    user: userDto | null;
}