"use client";

import React, { useState, useEffect, useCallback } from "react";
import { useNavigate } from "react-router-dom";
import { useAppContext } from "@/components/layout/AppLayout";
import Statistics from "@/components/quiz/Statistics";
import type { QuizAttempt } from "@/components/quiz/Statistics";
import { useSelector } from "react-redux";
import { RootState } from "@/redux/store";

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

function saveAttempts(attempts: QuizAttempt[]) {
  try {
    localStorage.setItem(STORAGE_KEY, JSON.stringify(attempts));
  } catch {
    // silently fail
  }
}

export default function ProfilePage() {
  const navigate = useNavigate();
  const { user } = useSelector((state: RootState) => state.appState);
  //const { user } = useAppContext();
  const [attempts, setAttempts] = useState<QuizAttempt[]>([]);

  useEffect(() => {
    setAttempts(loadAttempts());
  }, []);

  // Redirect if not logged in
  useEffect(() => {
    if (!user) {
      navigate("/");
    }
  }, [user, navigate]);

  const handleClearHistory = useCallback(() => {
    setAttempts([]);
    saveAttempts([]);
  }, []);

  const handleGoToQuiz = useCallback(() => {
    navigate("/");
  }, [navigate]);

  if (!user) {
    return null;
  }

  return (
    <Statistics
      attempts={attempts}
      onClearHistory={handleClearHistory}
      onGoToQuiz={handleGoToQuiz}
      user={user}
    />
  );
}
