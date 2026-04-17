"use client";

import React, { useState, useCallback, useEffect } from "react";
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  Button,
  Alert,
  Box,
  Typography,
} from "@mui/material";
import { RootState, useAppDispatch } from "@/redux/store";
import { addError, cleanAuthState, excludeError, registerInUser, setAuthFormFields, signInUser } from "@/redux/appSlice";
import { useSelector } from "react-redux";

export interface UserData {
  username: string;
  email: string;
}

interface AuthDialogProps {
  open: boolean;
  mode: "login" | "register";
  pendingAction?: boolean;
  onClose: () => void;
  onModeChange: (mode: "login" | "register") => void;
  onSuccess: (user: UserData) => void;
}

const textFieldSx = {
  mb: 2.5,
  "& .MuiOutlinedInput-root": {
    backgroundColor: "transparent",
    "& fieldset": {
      borderColor: "rgba(148, 163, 184, 0.25)",
      borderWidth: 1,
    },
    "&:hover fieldset": {
      borderColor: "rgba(148, 163, 184, 0.4)",
    },
    "&.Mui-focused fieldset": {
      borderColor: "#10b981",
      borderWidth: 2,
    },
  },
  "& .MuiInputLabel-root": {
    color: "#64748b",
    "&.Mui-focused": {
      color: "#10b981",
    },
  },
  "& .MuiInputBase-input": {
    color: "#f1f5f9",
  },
};

export default function AuthDialog({
  open,
  mode,
  pendingAction = false,
  onClose,
  onModeChange,
  onSuccess,
}: AuthDialogProps) {
  const { authState } = useSelector((state: RootState) => state.appState);
  const dispatch = useAppDispatch();

  //const [formData, setFormData] = useState<AuthFormData>(initialFormData);

  const handleClose = useCallback(() => {
    onClose();
    dispatch(cleanAuthState());
  }, [onClose, dispatch]);

  const handleModeChange = useCallback(
    (newMode: "login" | "register") => {
      dispatch(cleanAuthState());
      onModeChange(newMode);
    },
    [onModeChange]
  );

  const handleSubmit = useCallback(async () => {

    let hasError = false;
    let currentError = "";

    if (mode === "register") {

      currentError = "Please fill in all fields.";
      if (!authState.username || !authState.email || !authState.password) {
        dispatch(addError(currentError));
        hasError = true;
      }
      else {
        dispatch(excludeError(currentError));
      }

      currentError = "Username must be at least 3 characters.";
      if (authState.username && authState.username.length < 3) {
        dispatch(addError(currentError));
        hasError = true;
      }
      else {
        dispatch(excludeError(currentError));
      }

      currentError = "Username must be less than 32 characters.";
      if (authState.username && authState.username.length > 32) {
        dispatch(addError(currentError));
        hasError = true;
      }
      else {
        dispatch(excludeError(currentError));
      }

      currentError = "Invalid email format.";
      if (authState.email && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(authState.email)) {
        dispatch(addError(currentError));
        hasError = true;
      }
      else {
        dispatch(excludeError(currentError));
      }

      currentError = "Email must be less than 255 characters.";
      if (authState.email && authState.email.length > 255) {
        dispatch(addError(currentError));
        hasError = true;
      }
      else {
        dispatch(excludeError(currentError));
      }

      currentError = "Passwords do not match.";
      if (authState.password !== authState.confirmPassword) {
        dispatch(addError(currentError));
        hasError = true;
      }
      else {
        dispatch(excludeError(currentError));
      }

      currentError = "Password must be at least 6 characters.";
      if (authState.password && authState.password.length < 6) {
        dispatch(addError(currentError));
        hasError = true;
      }
      else {
        dispatch(excludeError(currentError));
      }

      currentError = "Password must be less than 32 characters.";
      if (authState.password.length > 32) {
        dispatch(addError(currentError));
        hasError = true;
      }
      else {
        dispatch(excludeError(currentError));
      }

      if (hasError) {
        return;
      }

      try {
        await dispatch(registerInUser(authState)).unwrap();
        onSuccess({
          username: authState.username,
          email: authState.email,
        });
      } catch (error) {
        console.error("Registration error:", error);
      }
    } else {
      currentError = "Please fill in all fields.";
      if (!authState.username || !authState.password) {
        dispatch(addError(currentError));
        return;
      }

      try {
        await dispatch(signInUser(authState)).unwrap();
        onSuccess({
          username: authState.username,
          email: authState.username.toLowerCase(),
        });
      } catch (error) {
        console.error("Login error:", error);
      }
    }

    //setFormData(initialFormData);
  }, [mode, authState, onSuccess]);

  const handleKeyDown = useCallback(
    (e: React.KeyboardEvent) => {
      if (e.key === "Enter") {
        handleSubmit();
      }
    },
    [handleSubmit]
  );

  return (
    <Dialog
      open={open}
      onClose={handleClose}
      maxWidth="xs"
      fullWidth
      PaperProps={{
        sx: {
          backgroundColor: "#1e293b",
          border: "1px solid rgba(148, 163, 184, 0.15)",
          borderRadius: 3,
        },
      }}
    >
      <DialogTitle sx={{ color: "#f1f5f9", fontWeight: 700, pb: 0 }}>
        {mode === "login" ? "Welcome Back" : "Create Account"}
      </DialogTitle>

      <DialogContent>
        <Typography variant="body2" sx={{ color: "#94a3b8", mb: 2, mt: 1 }}>
          {pendingAction
            ? "Please sign in to start a quiz."
            : mode === "login"
              ? "Sign in to track your quiz progress and statistics."
              : "Join Quiz-IT to save your progress and compete."}
        </Typography>

        <TextField
          fullWidth
          label="Username"
          value={authState.username}
          onChange={(e) =>
            dispatch(setAuthFormFields({ ...authState, username: e.target.value }))
          }
          onKeyDown={handleKeyDown}
          autoComplete="off"
          sx={textFieldSx}
        />

        {mode === "register" && (
          <TextField
            fullWidth
            label="Email"
            type="email"
            value={authState.email}
            onChange={(e) =>
              dispatch(setAuthFormFields({ ...authState, email: e.target.value }))
            }
            onKeyDown={handleKeyDown}
            autoComplete="off"
            sx={textFieldSx}
          />
        )}

        <TextField
          fullWidth
          label="Password"
          type="password"
          value={authState.password}
          onChange={(e) =>
            dispatch(setAuthFormFields({ ...authState, password: e.target.value }))
          }
          onKeyDown={handleKeyDown}
          autoComplete="new-password"
          sx={textFieldSx}
          inputProps={{ maxLength: 32 }}
        />

        {mode === "register" && (
          <TextField
            fullWidth
            label="Confirm Password"
            type="password"
            value={authState.confirmPassword}
            onChange={(e) =>
              dispatch(setAuthFormFields({ ...authState, confirmPassword: e.target.value }))
            }
            onKeyDown={handleKeyDown}
            autoComplete="new-password"
            sx={{ ...textFieldSx, mb: 1 }}
            inputProps={{ maxLength: 32 }}
          />
        )}
      </DialogContent>

      {authState.errors.length > 0 && (
        authState.errors.map((err, idx) => (
          <Alert key={idx} severity="error" sx={{ mb: 2 }}>
            {err}
          </Alert>
        ))
      )}

      <DialogActions sx={{ px: 3, pb: 2, flexDirection: "column", gap: 1 }}>
        <Box sx={{ display: "flex", gap: 1, width: "100%" }}>
          <Button
            onClick={handleClose}
            sx={{
              color: "#94a3b8",
              textTransform: "none",
              fontWeight: 600,
            }}
          >
            Cancel
          </Button>
          <Button
            variant="contained"
            onClick={handleSubmit}
            sx={{
              flex: 1,
              backgroundColor: "#10b981",
              color: "#fff",
              fontWeight: 700,
              "&:hover": { backgroundColor: "#059669" },
            }}
          >
            {mode === "login" ? "Sign In" : "Create Account"}
          </Button>
        </Box>

        <Typography
          variant="caption"
          sx={{ color: "#64748b", textAlign: "center" }}
        >
          {mode === "login" ? (
            <>
              {"Don't have an account? "}
              <Box
                component="span"
                onClick={() => handleModeChange("register")}
                sx={{
                  color: "#10b981",
                  cursor: "pointer",
                  fontWeight: 600,
                  "&:hover": { textDecoration: "underline" },
                }}
              >
                Register
              </Box>
            </>
          ) : (
            <>
              {"Already have an account? "}
              <Box
                component="span"
                onClick={() => handleModeChange("login")}
                sx={{
                  color: "#10b981",
                  cursor: "pointer",
                  fontWeight: 600,
                  "&:hover": { textDecoration: "underline" },
                }}
              >
                Sign In
              </Box>
            </>
          )}
        </Typography>
      </DialogActions>
    </Dialog>
  );
}

