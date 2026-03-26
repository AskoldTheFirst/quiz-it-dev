import { TechnologyDto } from "../dto/TechnologyDto";
import { Technology } from "../models/Technology";


const defaultMeta = {
    color: "#999",
    icon: "code",
};

const technologyMeta: Record<string, { color: string; icon: string }> = {
    "javascript": { color: "#f0db4f", icon: "javascript" },
    "c#": { color: "#844494", icon: "csharp" },
    "react": { color: "#61dafb", icon: "react" },
    "c/c++": { color: "#08845b", icon: "cpp" },
    "sql": { color: "#336791", icon: "database" },
    ".net": { color: "#512bd4", icon: "dotnet" },
    "english grammar": { color: "#e44d26", icon: "language" },
};

function normalizeKey(name: string): string {
    return name.trim().toLowerCase();
}

export function mapTechnology(dto: TechnologyDto): Technology {
    const meta = technologyMeta[normalizeKey(dto.name)] ?? defaultMeta;

    return {
        ...dto,
        ...meta,
    };
}

export function mapTechnologyNameToColor(name: string): string {
    const meta = technologyMeta[normalizeKey(name)] ?? defaultMeta.color;
    return meta.color;
}