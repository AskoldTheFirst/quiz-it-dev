"use client";

import { useEffect } from "react";
import Leaderboard from "@/components/quiz/Leaderboard";
import { resetFilters } from "@/redux/topStatisticsSlice";
import { useAppDispatch } from "@/redux/store";

export default function TopPage() {
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(resetFilters());
  }, [dispatch]);

  return <Leaderboard />;
}
