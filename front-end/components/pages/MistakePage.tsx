"use client";

import { useEffect } from "react";
import Mistakes from "../quiz/Mistakes";
import { useAppDispatch } from "@/redux/store";
import { loadMistakes } from "@/redux/mistakesSlice";

export default function MistakePage() {
    return <Mistakes />;
}
