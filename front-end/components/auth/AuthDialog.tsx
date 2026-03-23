"use client";

import React, { useState, useCallback } from "react";
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
import { useAppDispatch } from "@/redux/store";
import { registerInUser, signInUser } from "@/redux/appSlice";

export interface UserData {
  username: string;
  email: string;
}

export interface AuthFormData {
  username: string;
  email: string;
  password: string;
  confirmPassword: string;
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

const initialFormData: AuthFormData = {
  username: "",
  email: "",
  password: "",
  confirmPassword: "",
};

export default function AuthDialog({
  open,
  mode,
  pendingAction = false,
  onClose,
  onModeChange,
  onSuccess,
}: AuthDialogProps) {
  const dispatch = useAppDispatch();
  const [formData, setFormData] = useState<AuthFormData>(initialFormData);
  const [error, setError] = useState("");

  const handleClose = useCallback(() => {
    setFormData(initialFormData);
    setError("");
    onClose();
  }, [onClose]);

  const handleModeChange = useCallback(
    (newMode: "login" | "register") => {
      setFormData(initialFormData);
      setError("");
      onModeChange(newMode);
    },
    [onModeChange]
  );

  const handleSubmit = useCallback(async () => {
    setError("");

    if (mode === "register") {
      if (!formData.username || !formData.email || !formData.password) {
        setError("Please fill in all fields.");
        return;
      }
      if (formData.password !== formData.confirmPassword) {
        setError("Passwords do not match.");
        return;
      }
      if (formData.password.length < 6) {
        setError("Password must be at least 6 characters.");
        return;
      }

      try {
        await dispatch(registerInUser(formData)).unwrap();
        onSuccess({
          username: formData.username,
          email: formData.email,
        });
      } catch (error) {
        console.error("Registration error:", error);
      }
    } else {
      if (!formData.username || !formData.password) {
        setError("Please fill in all fields.");
        return;
      }
      
      try {
        await dispatch(signInUser(formData)).unwrap();
        onSuccess({
          username: formData.username,
          email: formData.username.toLowerCase(),
        });
      } catch (error) {
        console.error("Login error:", error);
      }
    }

    setFormData(initialFormData);
  }, [mode, formData, onSuccess]);

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

        {error && (
          <Alert severity="error" sx={{ mb: 2 }}>
            {error}
          </Alert>
        )}

        <TextField
          fullWidth
          label="Username"
          value={formData.username}
          onChange={(e) =>
            setFormData({ ...formData, username: e.target.value })
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
            value={formData.email}
            onChange={(e) =>
              setFormData({ ...formData, email: e.target.value })
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
          value={formData.password}
          onChange={(e) =>
            setFormData({ ...formData, password: e.target.value })
          }
          onKeyDown={handleKeyDown}
          autoComplete="new-password"
          sx={textFieldSx}
        />

        {mode === "register" && (
          <TextField
            fullWidth
            label="Confirm Password"
            type="password"
            value={formData.confirmPassword}
            onChange={(e) =>
              setFormData({ ...formData, confirmPassword: e.target.value })
            }
            onKeyDown={handleKeyDown}
            autoComplete="new-password"
            sx={{ ...textFieldSx, mb: 1 }}
          />
        )}
      </DialogContent>

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
