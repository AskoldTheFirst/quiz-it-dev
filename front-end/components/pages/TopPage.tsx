"use client";

import React, { useState, useEffect } from "react";
import Leaderboard from "@/components/quiz/Leaderboard";
import type { QuizAttempt } from "@/components/quiz/Statistics";

const STORAGE_KEY = "quizmaster-attempts";

function loadAttempts(): QuizAttempt[] {
  if (typeof window === "undefined") return [];
  try {
    const stored = localStorage.getItem(STORAGE_KEY);
    return stored ? JSON.parse(stored) : [];
  } catch {
    return [];
  }
}

export default function TopPage() {
  const [attempts, setAttempts] = useState<QuizAttempt[]>([]);

  useEffect(() => {
    setAttempts(loadAttempts());
  }, []);

  return <Leaderboard attempts={attempts} />;
}
