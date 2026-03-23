"use client";

import React, { useCallback } from "react";
import { useNavigate } from "react-router-dom";
import About from "@/components/quiz/About";

export default function AboutPage() {
  const navigate = useNavigate();

  const handleGoToQuiz = useCallback(() => {
    navigate("/");
  }, [navigate]);

  return <About onGoToQuiz={handleGoToQuiz} />;
}
