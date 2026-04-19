import { addRegisterError, closeRegisterForm, excludeRegistrationError, openLoginForm, registerInUser, setRegisterFormFields } from "@/redux/appSlice";
import { RootState, useAppDispatch } from "@/redux/store";
import { Alert, Box, Button, Dialog, DialogActions, DialogContent, DialogTitle, TextField, Typography } from "@mui/material";
import { useCallback } from "react";
import { useSelector } from "react-redux";

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

export default function RegisterDialog() {
    const dispatch = useAppDispatch();
    const { registerFormFields: form } = useSelector((state: RootState) => state.appState);

    const handleSubmit = useCallback(async () => {

        if (form === null) return;

        let hasError = false;
        let currentError = "";

        currentError = "Please fill in all fields.";
        if (!form.username || !form.email || !form.password) {
            dispatch(addRegisterError(currentError));
            hasError = true;
        }
        else {
            dispatch(excludeRegistrationError(currentError));
        }

        currentError = "Username must be at least 3 characters.";
        if (form.username && form.username.length < 3) {
            dispatch(addRegisterError(currentError));
            hasError = true;
        }
        else {
            dispatch(excludeRegistrationError(currentError));
        }

        currentError = "Username must be less than 32 characters.";
        if (form.username && form.username.length > 32) {
            dispatch(addRegisterError(currentError));
            hasError = true;
        }
        else {
            dispatch(excludeRegistrationError(currentError));
        }

        currentError = "Invalid email format.";
        if (form.email && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(form.email)) {
            dispatch(addRegisterError(currentError));
            hasError = true;
        }
        else {
            dispatch(excludeRegistrationError(currentError));
        }

        currentError = "Email must be less than 255 characters.";
        if (form.email && form.email.length > 255) {
            dispatch(addRegisterError(currentError));
            hasError = true;
        }
        else {
            dispatch(excludeRegistrationError(currentError));
        }

        currentError = "Passwords do not match.";
        if (form.password !== form.confirmPassword) {
            dispatch(addRegisterError(currentError));
            hasError = true;
        }
        else {
            dispatch(excludeRegistrationError(currentError));
        }

        currentError = "Password must be at least 6 characters.";
        if (form.password && form.password.length < 6) {
            dispatch(addRegisterError(currentError));
            hasError = true;
        }
        else {
            dispatch(excludeRegistrationError(currentError));
        }

        currentError = "Password must be less than 32 characters.";
        if (form.password.length > 32) {
            dispatch(addRegisterError(currentError));
            hasError = true;
        }
        else {
            dispatch(excludeRegistrationError(currentError));
        }

        if (hasError) {
            return;
        }

        try {
            await dispatch(registerInUser(form));
        } catch (error) {
            console.error("Registration error:", error);
        }
    }, [dispatch, form]);

    const handleKeyDown = useCallback(
        (e: React.KeyboardEvent) => {
            if (e.key === "Enter") {
                handleSubmit();
            }
        },
        [handleSubmit]
    );

    const handleModeChange = useCallback(() => { }, []);

    if (form === null) return null;

    return (<Dialog
        open={form !== null}
        onClose={() => dispatch(closeRegisterForm())}
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
            Create Account
        </DialogTitle>

        <DialogContent>
            <Typography variant="body2" sx={{ color: "#94a3b8", mb: 2, mt: 1 }}>
                Join Quiz-IT to save your progress and compete.
            </Typography>

            <TextField
                fullWidth
                label="Username"
                value={form.username}
                onChange={(e) =>
                    dispatch(setRegisterFormFields({ ...form, username: e.target.value }))
                }
                onKeyDown={handleKeyDown}
                autoComplete="off"
                sx={textFieldSx}
            />

            <TextField
                fullWidth
                label="Email"
                type="email"
                value={form.email}
                onChange={(e) =>
                    dispatch(setRegisterFormFields({ ...form, email: e.target.value }))
                }
                onKeyDown={handleKeyDown}
                autoComplete="off"
                sx={textFieldSx}
            />

            <TextField
                fullWidth
                label="Password"
                type="password"
                value={form.password}
                onChange={(e) =>
                    dispatch(setRegisterFormFields({ ...form, password: e.target.value }))
                }
                onKeyDown={handleKeyDown}
                autoComplete="new-password"
                sx={textFieldSx}
                inputProps={{ maxLength: 32 }}
            />

            <TextField
                fullWidth
                label="Confirm Password"
                type="password"
                value={form.confirmPassword}
                onChange={(e) =>
                    dispatch(setRegisterFormFields({ ...form, confirmPassword: e.target.value }))
                }
                onKeyDown={handleKeyDown}
                autoComplete="new-password"
                sx={{ ...textFieldSx, mb: 1 }}
                inputProps={{ maxLength: 32 }}
            />

        </DialogContent>

        {form.errors.length > 0 && (
            form.errors.map((err, idx) => (
                <Alert key={idx} severity="error" sx={{ mb: 2 }}>
                    {err}
                </Alert>
            ))
        )}

        <DialogActions sx={{ px: 3, pb: 2, flexDirection: "column", gap: 1 }}>
            <Box sx={{ display: "flex", gap: 1, width: "100%" }}>
                <Button
                    onClick={() => dispatch(closeRegisterForm())}
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
                    Create Account
                </Button>
            </Box>

            <Typography
                variant="caption"
                sx={{ color: "#64748b", textAlign: "center" }}
            >
                <>
                    Already have an account?
                    <Box
                        component="span"
                        onClick={() => dispatch(openLoginForm())}
                        sx={{
                            color: "#10b981",
                            cursor: "pointer",
                            fontWeight: 600,
                            "&:hover": { textDecoration: "underline" },
                        }}
                    >
                        {" "}Sign In
                    </Box>
                </>
            </Typography>
        </DialogActions>
    </Dialog>
    );
}