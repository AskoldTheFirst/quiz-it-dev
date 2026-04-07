"use client";

import { useCallback, useEffect } from "react";
import { Box, Typography } from "@mui/material";
import { useAppContext } from "@/components/layout/AppLayout";
import TopicCard from "@/components/quiz/TopicCard";
import QuizQuestion from "@/components/quiz/QuizQuestion";
import QuizResults from "@/components/quiz/QuizResults";
import { useSelector } from "react-redux";
import { RootState, useAppDispatch } from "@/redux/store";
import { complete, createTest, decrementTime, setTestData } from "@/redux/testSlice";
import { setForbidenPages } from "@/redux/appSlice";
import { NavItem } from "@/biz/models/NavItems";

export default function QuizPage() {
  const { topics, user } = useSelector((state: RootState) => state.appState);
  const { test, result } = useSelector((state: RootState) => state.testState);
  const dispatch = useAppDispatch();

  const { openLogin } = useAppContext();

  useEffect(() => {
    if (result) {
      dispatch(setForbidenPages([]));
    }
  }, [dispatch, result]);

  useEffect(() => {
    if (test) {
      dispatch(setForbidenPages([NavItem.Top, NavItem.Profile, NavItem.About]));
    }
  }, [test, dispatch]);

  useEffect(() => {
    return () => {
      if (result) {
        dispatch(setTestData({ test: null, result: null }));
      }
    };
  }, [dispatch]);

  // Timer effect
  useEffect(() => {

    if (!test) {
      return;
    }

    const timer = setInterval(() => {
      dispatch(decrementTime());

      if (test.secondsLeft < 1) {
        clearInterval(timer);
        dispatch(complete(test.testId));
        return 0;
      }
      return test.secondsLeft - 1;
    }, 1000);

    return () => clearInterval(timer);
  }, [test, dispatch]);

  const handleSelectTopic = useCallback(
    async (topicName: string) => {
      if (!user) {
        openLogin();
        return;
      }

      await dispatch(createTest(topicName));
      dispatch(setForbidenPages([NavItem.Top, NavItem.Profile, NavItem.About]));
    }, [user, openLogin, dispatch]);

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
          {topics.map((topic) => (
            <TopicCard key={topic.id} topic={topic} onSelect={handleSelectTopic} />
          ))}
        </Box>
      </>
    );
  }

  // Question view
  if (test) {
    return <QuizQuestion test={test} />;
  }

  // Results view
  if (result) {
    return <QuizResults result={result} />;
  }

  return null;
}

