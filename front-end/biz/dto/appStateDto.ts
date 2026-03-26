import { TechnologyDto } from "./TechnologyDto";
import { UserDto } from "./UserDto";

export interface AppStateDto {
    technologies: TechnologyDto[];
    user: UserDto | null;
}