import { addLoginError, closeLoginForm, openRegisterForm, setLoginFormFields, signInUser } from "@/redux/appSlice";
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

export default function LoginDialog() {
    const dispatch = useAppDispatch();
    const { loginFormFields: form } = useSelector((state: RootState) => state.appState);

    const handleSubmit = useCallback(async () => {

        if (form === null) return;

        if (!form.username || !form.password) {
            dispatch(addLoginError("Please fill in all fields."));
            return;
        }

        try {
            await dispatch(signInUser(form));
        } catch (error) {
            console.error("Login error:", error);
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
        onClose={() => dispatch(closeLoginForm())}
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
            Welcome Back
        </DialogTitle>

        <DialogContent>
            <Typography variant="body2" sx={{ color: "#94a3b8", mb: 2, mt: 1 }}>
                Sign in to track your quiz progress and statistics.
            </Typography>

            <TextField
                fullWidth
                label="Username"
                value={form.username}
                onChange={(e) =>
                    dispatch(setLoginFormFields({ ...form, username: e.target.value }))
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
                    dispatch(setLoginFormFields({ ...form, password: e.target.value }))
                }
                onKeyDown={handleKeyDown}
                autoComplete="new-password"
                sx={textFieldSx}
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
                    onClick={() => dispatch(closeLoginForm())}
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
                    Sign In
                </Button>
            </Box>

            <Typography
                variant="caption"
                sx={{ color: "#64748b", textAlign: "center" }}
            >
                <>
                    Don't have an account?
                    <Box
                        component="span"
                        onClick={() => dispatch(openRegisterForm())}
                        sx={{
                            color: "#10b981",
                            cursor: "pointer",
                            fontWeight: 600,
                            "&:hover": { textDecoration: "underline" },
                        }}
                    >
                        {" "}Register
                    </Box>
                </>
            </Typography>
        </DialogActions>
    </Dialog>
    );
}