"use client";

import { useEffect } from "react";
import Mistakes from "../quiz/Mistakes";
import { resetFilters } from "@/redux/mistakesSlice";
import { useAppDispatch } from "@/redux/store";

export default function MistakePage() {
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(resetFilters());
  }, [dispatch]);

  return <Mistakes />;
}
