"use client";

import React, { useState, useCallback, useEffect } from "react";
import { Box, Typography } from "@mui/material";
import { useAppContext } from "@/components/layout/AppLayout";
import TopicCard from "@/components/quiz/TopicCard";
import QuizQuestion from "@/components/quiz/QuizQuestion";
import QuizResults from "@/components/quiz/QuizResults";
import { quizTopics } from "@/lib/quizData";
import type { QuizTopic } from "@/lib/quizData";
import type { QuizAttempt } from "@/components/quiz/Statistics";
import { useSelector } from "react-redux";
import { RootState, useAppDispatch } from "@/redux/store";
import { cancelTest, createTest } from "@/redux/testSlice";

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

export default function QuizPage() {
  const { technologies, user } = useSelector((state: RootState) => state.appState);
  const { test, result } = useSelector((state: RootState) => state.testState);
  const dispatch = useAppDispatch();

  const { quizState, setQuizState, openLogin } = useAppContext();

  const [selectedTopic, setSelectedTopic] = useState<QuizTopic | null>(null);
  const [currentQuestionIndex, setCurrentQuestionIndex] = useState(0);
  //const [selectedAnswer, setSelectedAnswer] = useState<number | null>(null);
  const [isAnswered, setIsAnswered] = useState(false);
  const [score, setScore] = useState(0);
  const [earnedWeight, setEarnedWeight] = useState(0);
  const [answers, setAnswers] = useState<(number | null)[]>([]);
  const [timeRemaining, setTimeRemaining] = useState(600);
  const [attempts, setAttempts] = useState<QuizAttempt[]>([]);
  const [pendingTopicId, setPendingTopicId] = useState<string | null>(null);

  useEffect(() => {
    setAttempts(loadAttempts());
  }, []);

  // Timer effect
  useEffect(() => {
    if (quizState !== "active" || !selectedTopic) return;

    const timer = setInterval(() => {
      setTimeRemaining((prev) => {
        if (prev <= 1) {
          clearInterval(timer);
          return 0;
        }
        return prev - 1;
      });
    }, 1000);

    return () => clearInterval(timer);
  }, [quizState, selectedTopic]);

  // Handle time up
  useEffect(() => {
    if (timeRemaining === 0 && quizState === "active" && selectedTopic) {
      finishQuiz();
    }
  }, [timeRemaining, quizState, selectedTopic]);

  // Handle pending topic after login
  useEffect(() => {
    if (user && pendingTopicId) {
      const topic = quizTopics.find((t) => t.id === pendingTopicId);
      if (topic) {
        startQuiz(topic);
      }
      setPendingTopicId(null);
    }
  }, [user, pendingTopicId]);

  const startQuiz = (topic: QuizTopic) => {
    setSelectedTopic(topic);
    setCurrentQuestionIndex(0);
    //setSelectedAnswer(null);
    setIsAnswered(false);
    setScore(0);
    setEarnedWeight(0);
    setAnswers(new Array(topic.questions.length).fill(null));
    setTimeRemaining(600);
    setQuizState("active");
  };

  const finishQuiz = () => {
    if (!selectedTopic) return;

    let finalScore = 0;
    let finalWeight = 0;
    answers.forEach((answer, index) => {
      if (answer === selectedTopic.questions[index].correctAnswer) {
        finalScore += 1;
        finalWeight += selectedTopic.questions[index].weight;
      }
    });
    setScore(finalScore);
    setEarnedWeight(finalWeight);

    const totalWeight = selectedTopic.questions.reduce((sum, q) => sum + q.weight, 0);
    const pct = Math.round((finalScore / selectedTopic.questions.length) * 100);
    const wPct = totalWeight > 0 ? Math.round((finalWeight / totalWeight) * 100) : pct;

    const newAttempt: QuizAttempt = {
      topicId: selectedTopic.id,
      topicTitle: selectedTopic.title,
      topicColor: selectedTopic.color,
      score: finalScore,
      totalQuestions: selectedTopic.questions.length,
      percentage: pct,
      weightedPercentage: wPct,
      date: new Date().toLocaleDateString("en-US", {
        month: "short",
        day: "numeric",
        year: "numeric",
        hour: "2-digit",
        minute: "2-digit",
      }),
    };
    const updated = [...attempts, newAttempt];
    setAttempts(updated);
    saveAttempts(updated);
    setQuizState("results");
  };

  const handleSelectTopic = useCallback(
    async (topicName: string) => {
      if (!user) {
        setPendingTopicId(topicName);
        openLogin();
        return;
      }

      await dispatch(createTest(topicName));

      // const topic = quizTopics.find((t) => t.id === topicName);
      // if (topic) {
      //   startQuiz(topic);
      // }
    },
    [user, openLogin]
  );

  // const handleAnswer = useCallback(
  //   (answerIndex: number) => {
  //     // if (!selectedTopic) return;
  //     // setSelectedAnswer(answerIndex);
  //     // setAnswers((prev) => {
  //     //   const newAnswers = [...prev];
  //     //   newAnswers[currentQuestionIndex] = answerIndex;
  //     //   return newAnswers;
  //     // });
  //     setSelectedAnswer(answerIndex);
  //   },
  //   []
  // );

  // const handleNext = useCallback(() => {
  //   if (!selectedTopic) return;

  //   if (currentQuestionIndex + 1 < selectedTopic.questions.length) {
  //     setCurrentQuestionIndex((prev) => prev + 1);
  //     //setSelectedAnswer(answers[currentQuestionIndex + 1] ?? null);
  //     setIsAnswered(false);
  //   } else {
  //     finishQuiz();
  //   }
  // }, [currentQuestionIndex, selectedTopic, answers]);

  const handleRetry = useCallback(() => {
    if (!selectedTopic) return;
    startQuiz(selectedTopic);
  }, [selectedTopic]);

  const handleHome = useCallback(() => {
    setSelectedTopic(null);
    setQuizState("topics");
  }, []);

  // const handleCancel = useCallback(async () => {
  //   setSelectedTopic(null);
  //   setQuizState("topics");
  //   setCurrentQuestionIndex(0);
  //   //setSelectedAnswer(null);
  //   setIsAnswered(false);
  //   setScore(0);
  //   setEarnedWeight(0);
  //   setAnswers([]);
  //   setTimeRemaining(600);
  // }, [test]);

  const totalWeight = selectedTopic
    ? selectedTopic.questions.reduce((sum, q) => sum + q.weight, 0)
    : 0;

    console.log(test);
  // Topics view
  if (!test && !result) {
    return (
      <>
        <Box sx={{ textAlign: "center", mb: 4 }}>
          <Typography
            variant="h3"
            sx={{ color: "#f1f5f9", fontWeight: 800, mb: 1.5 }}
          >
            Test Your Knowledge
          </Typography>
          <Typography
            variant="body1"
            sx={{ color: "#94a3b8", maxWidth: 500, mx: "auto" }}
          >
            Choose a quiz topic below and challenge yourself with questions on
            programming, databases, frameworks, and more.
          </Typography>
        </Box>

        <Box
          sx={{
            display: "grid",
            gridTemplateColumns: {
              xs: "1fr",
              sm: "1fr 1fr",
              md: "1fr 1fr 1fr",
            },
            gap: 2.5,
          }}
        >
          {technologies.map((topic) => (
            <TopicCard key={topic.id} topic={topic} onSelect={handleSelectTopic} />
          ))}
        </Box>
      </>
    );
  }

  // Question view
  if (test) {
    return (
      <QuizQuestion
        timeRemaining={timeRemaining}
        test={test}
      />
    );
  }

  // Results view
  if (result) {
    return (
      <QuizResults
        onRetry={handleRetry}
        onHome={handleHome}
        result={result}
      />
    );
  }

  return null;
}
