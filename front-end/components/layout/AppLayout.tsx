"use client";

import React, { useState, useCallback, useEffect } from "react";
import { Outlet, useNavigate, useLocation } from "react-router-dom";
import { Box, Typography, Container } from "@mui/material";
import Navbar from "@/components/quiz/Navbar";
import type { UserData } from "@/components/quiz/Navbar";
import AuthDialog from "@/components/auth/AuthDialog";
import { useSelector } from "react-redux";
import { RootState, useAppDispatch } from "@/redux/store";

export type QuizState = "topics" | "active" | "results";

interface AppContextType {
  //user: UserData | null;
  quizState: QuizState;
  setQuizState: (state: QuizState) => void;
  openLogin: () => void;
  openRegister: () => void;
}

export const AppContext = React.createContext<AppContextType | null>(null);

export function useAppContext() {
  const context = React.useContext(AppContext);
  if (!context) {
    throw new Error("useAppContext must be used within AppLayout");
  }
  return context;
}

export default function AppLayout() {
  const navigate = useNavigate();
  const location = useLocation();
  
  const { user } = useSelector((state: RootState) => state.appState);
  const [isInitialized, setIsInitialized] = useState(false);
  const [authDialog, setAuthDialog] = useState<"login" | "register" | null>(null);
  const [quizState, setQuizState] = useState<QuizState>("topics");
  const [pendingNavigation, setPendingNavigation] = useState<string | null>(null);

  useEffect(() => {
    setIsInitialized(true);
  }, []);

  const currentPage = location.pathname === "/" ? "quiz" : location.pathname.slice(1);

  const openLogin = useCallback(() => {
    setAuthDialog("login");
  }, []);

  const openRegister = useCallback(() => {
    setAuthDialog("register");
  }, []);

  const handleAuthSuccess = useCallback(
    (userData: UserData) => {
      setAuthDialog(null);

      if (pendingNavigation) {
        navigate(pendingNavigation);
        setPendingNavigation(null);
      }
    },
    [pendingNavigation, navigate]
  );

  const handleAuthClose = useCallback(() => {
    setAuthDialog(null);
    setPendingNavigation(null);
  }, []);

  const handleAuthModeChange = useCallback((mode: "login" | "register") => {
    setAuthDialog(mode);
  }, []);

  const handleNavigate = useCallback(
    (page: string) => {
      
      // Disable navigation during active quiz
      if (quizState === "active" && page !== "quiz") {
        return;
      }

      // Profile requires login
      if (page === "profile" && !user) {
        setPendingNavigation("/profile");
        openLogin();
        return;
      }

      const path = page === "quiz" ? "/" : `/${page}`;
      navigate(path);

      // Reset quiz state when navigating to quiz
      if (page === "quiz" && quizState !== "active") {
        setQuizState("topics");
      }
    },
    [quizState, user, navigate, openLogin]
  );

  const isQuizActive = quizState === "active";

  return (
    <AppContext.Provider value={{ quizState, setQuizState, openLogin, openRegister }}>
      <Box sx={{ minHeight: "100vh", display: "flex", flexDirection: "column" }}>
        <Navbar
          currentPage={currentPage}
          onNavigate={handleNavigate}
          onOpenLogin={openLogin}
          onOpenRegister={openRegister}
          isQuizActive={isQuizActive}
          isInitialized={isInitialized}
        />

        <Container maxWidth="lg" sx={{ flex: 1, py: 4 }}>
          <Outlet />
        </Container>

        {/* Footer */}
        <Box
          sx={{
            textAlign: "center",
            py: 3,
            borderTop: "1px solid rgba(148, 163, 184, 0.06)",
          }}
        >
          <Typography variant="caption" sx={{ color: "#475569" }}>
            Quiz-IT - Built for learning
          </Typography>
        </Box>

        {/* Auth Dialog */}
        <AuthDialog
          open={authDialog !== null}
          mode={authDialog || "login"}
          pendingAction={pendingNavigation !== null}
          onClose={handleAuthClose}
          onModeChange={handleAuthModeChange}
          onSuccess={handleAuthSuccess}
        />
      </Box>
    </AppContext.Provider>
  );
}
