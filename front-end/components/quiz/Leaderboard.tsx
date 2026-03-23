"use client";

import React from "react";
import {
  Box,
  Typography,
  Card,
  CardContent,
  Chip,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Avatar,
} from "@mui/material";
import EmojiEventsIcon from "@mui/icons-material/EmojiEvents";
import FilterListIcon from "@mui/icons-material/FilterList";
import type { QuizAttempt } from "./Statistics";
import { quizTopics } from "@/lib/quizData";

interface LeaderboardEntry {
  rank: number;
  username: string;
  topicTitle: string;
  topicColor: string;
  score: number;
  totalQuestions: number;
  percentage: number;
  weightedPercentage: number;
  date: string;
}

interface LeaderboardProps {
  attempts: QuizAttempt[];
}

const MOCK_LEADERBOARD: LeaderboardEntry[] = [
  { rank: 1, username: "AlexDev", topicTitle: "TypeScript", topicColor: "#3178c6", score: 10, totalQuestions: 10, percentage: 100, weightedPercentage: 100, date: "Feb 18, 2026" },
  { rank: 2, username: "SarahCode", topicTitle: "React", topicColor: "#61dafb", score: 10, totalQuestions: 10, percentage: 100, weightedPercentage: 100, date: "Feb 17, 2026" },
  { rank: 3, username: "MaxSharp", topicTitle: "C#", topicColor: "#68217a", score: 9, totalQuestions: 10, percentage: 90, weightedPercentage: 93, date: "Feb 17, 2026" },
  { rank: 4, username: "LiamJS", topicTitle: "JavaScript", topicColor: "#f0db4f", score: 9, totalQuestions: 10, percentage: 90, weightedPercentage: 88, date: "Feb 16, 2026" },
  { rank: 5, username: "EmmaDB", topicTitle: "SQL", topicColor: "#336791", score: 9, totalQuestions: 10, percentage: 90, weightedPercentage: 87, date: "Feb 16, 2026" },
  { rank: 6, username: "NoahNet", topicTitle: ".NET", topicColor: "#512bd4", score: 8, totalQuestions: 10, percentage: 80, weightedPercentage: 85, date: "Feb 15, 2026" },
  { rank: 7, username: "OliverTS", topicTitle: "TypeScript", topicColor: "#3178c6", score: 8, totalQuestions: 10, percentage: 80, weightedPercentage: 82, date: "Feb 15, 2026" },
  { rank: 8, username: "SophiaLang", topicTitle: "English Grammar", topicColor: "#e44d26", score: 8, totalQuestions: 10, percentage: 80, weightedPercentage: 80, date: "Feb 14, 2026" },
  { rank: 9, username: "JamesReact", topicTitle: "React", topicColor: "#61dafb", score: 8, totalQuestions: 10, percentage: 80, weightedPercentage: 78, date: "Feb 14, 2026" },
  { rank: 10, username: "AvaSQL", topicTitle: "SQL", topicColor: "#336791", score: 7, totalQuestions: 10, percentage: 70, weightedPercentage: 75, date: "Feb 13, 2026" },
];

const getRankColor = (rank: number) => {
  if (rank === 1) return "#fbbf24";
  if (rank === 2) return "#94a3b8";
  if (rank === 3) return "#cd7f32";
  return "#64748b";
};

const getRankIcon = (rank: number) => {
  if (rank <= 3) {
    return (
      <EmojiEventsIcon sx={{ fontSize: 18, color: getRankColor(rank) }} />
    );
  }
  return null;
};

const getGradeColor = (pct: number) => {
  if (pct >= 80) return "#10b981";
  if (pct >= 60) return "#f59e0b";
  return "#ef4444";
};

export default function Leaderboard({ attempts }: LeaderboardProps) {
  const [topicFilter, setTopicFilter] = React.useState<string>("all");
  const [scoreFilter, setScoreFilter] = React.useState<string>("all");

  // Merge user attempts with mock data
  const userEntries: LeaderboardEntry[] = attempts.map((a) => ({
    rank: 0,
    username: "You",
    topicTitle: a.topicTitle,
    topicColor: a.topicColor,
    score: a.score,
    totalQuestions: a.totalQuestions,
    percentage: a.percentage,
    weightedPercentage: a.weightedPercentage,
    date: a.date,
  }));

  const allEntries = [...MOCK_LEADERBOARD, ...userEntries];

  // Apply filters
  let filtered = allEntries;
  if (topicFilter !== "all") {
    filtered = filtered.filter((e) => e.topicTitle === topicFilter);
  }
  if (scoreFilter === "90+") {
    filtered = filtered.filter((e) => e.percentage >= 90);
  } else if (scoreFilter === "80+") {
    filtered = filtered.filter((e) => e.percentage >= 80);
  } else if (scoreFilter === "70+") {
    filtered = filtered.filter((e) => e.percentage >= 70);
  }

  // Sort by weighted percentage, then percentage, then score
  filtered.sort((a, b) => {
    if (b.weightedPercentage !== a.weightedPercentage)
      return b.weightedPercentage - a.weightedPercentage;
    if (b.percentage !== a.percentage) return b.percentage - a.percentage;
    return b.score - a.score;
  });

  // Assign ranks
  const ranked = filtered.map((entry, i) => ({ ...entry, rank: i + 1 }));

  const topicOptions = quizTopics.map((t) => ({
    label: t.title,
    value: t.title,
  }));

  const selectSx = {
    color: "#f1f5f9",
    "& .MuiOutlinedInput-notchedOutline": {
      borderColor: "rgba(148, 163, 184, 0.2)",
    },
    "&:hover .MuiOutlinedInput-notchedOutline": {
      borderColor: "rgba(16, 185, 129, 0.4)",
    },
    "&.Mui-focused .MuiOutlinedInput-notchedOutline": {
      borderColor: "#10b981",
    },
    "& .MuiSvgIcon-root": { color: "#64748b" },
  };

  const menuPropsSx = {
    PaperProps: {
      sx: {
        backgroundColor: "#1e293b",
        border: "1px solid rgba(148,163,184,0.15)",
        "& .MuiMenuItem-root": {
          color: "#e2e8f0",
          fontSize: "0.85rem",
          "&:hover": { backgroundColor: "rgba(16,185,129,0.08)" },
          "&.Mui-selected": {
            backgroundColor: "rgba(16,185,129,0.12)",
            "&:hover": { backgroundColor: "rgba(16,185,129,0.15)" },
          },
        },
      },
    },
  };

  return (
    <Box>
      {/* Header */}
      <Box sx={{ mb: 3 }}>
        <Typography variant="h4" sx={{ color: "#f1f5f9", fontWeight: 800, mb: 0.5 }}>
          Leaderboard
        </Typography>
        <Typography variant="body1" sx={{ color: "#94a3b8" }}>
          Top quiz results from the community
        </Typography>
      </Box>

      {/* Filters */}
      <Card sx={{ backgroundColor: "#1e293b", border: "1px solid rgba(148, 163, 184, 0.1)", mb: 3 }}>
        <CardContent sx={{ p: 2.5, "&:last-child": { pb: 2.5 } }}>
          <Box sx={{ display: "flex", alignItems: "center", gap: 2, flexWrap: "wrap" }}>
            <Box sx={{ display: "flex", alignItems: "center", gap: 1, color: "#64748b" }}>
              <FilterListIcon sx={{ fontSize: 18 }} />
              <Typography variant="body2" sx={{ fontWeight: 600 }}>
                Filters
              </Typography>
            </Box>
            <FormControl size="small" sx={{ minWidth: 160 }}>
              <InputLabel sx={{ color: "#64748b" }}>Topic</InputLabel>
              <Select
                value={topicFilter}
                label="Topic"
                onChange={(e) => setTopicFilter(e.target.value)}
                sx={selectSx}
                MenuProps={menuPropsSx}
              >
                <MenuItem value="all">All Topics</MenuItem>
                {topicOptions.map((t) => (
                  <MenuItem key={t.value} value={t.value}>
                    {t.label}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
            <FormControl size="small" sx={{ minWidth: 130 }}>
              <InputLabel sx={{ color: "#64748b" }}>Score</InputLabel>
              <Select
                value={scoreFilter}
                label="Score"
                onChange={(e) => setScoreFilter(e.target.value)}
                sx={selectSx}
                MenuProps={menuPropsSx}
              >
                <MenuItem value="all">All Scores</MenuItem>
                <MenuItem value="90+">90%+</MenuItem>
                <MenuItem value="80+">80%+</MenuItem>
                <MenuItem value="70+">70%+</MenuItem>
              </Select>
            </FormControl>
          </Box>
        </CardContent>
      </Card>

      {/* Leaderboard Table */}
      <Card sx={{ backgroundColor: "#1e293b", border: "1px solid rgba(148, 163, 184, 0.1)" }}>
        <TableContainer>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell sx={{ color: "#64748b", fontWeight: 700, borderColor: "rgba(148,163,184,0.08)", fontSize: "0.8rem" }}>
                  Rank
                </TableCell>
                <TableCell sx={{ color: "#64748b", fontWeight: 700, borderColor: "rgba(148,163,184,0.08)", fontSize: "0.8rem" }}>
                  User
                </TableCell>
                <TableCell sx={{ color: "#64748b", fontWeight: 700, borderColor: "rgba(148,163,184,0.08)", fontSize: "0.8rem" }}>
                  Topic
                </TableCell>
                <TableCell sx={{ color: "#64748b", fontWeight: 700, borderColor: "rgba(148,163,184,0.08)", fontSize: "0.8rem" }}>
                  Score
                </TableCell>
                <TableCell sx={{ color: "#64748b", fontWeight: 700, borderColor: "rgba(148,163,184,0.08)", fontSize: "0.8rem" }}>
                  Percentage
                </TableCell>
                <TableCell sx={{ color: "#64748b", fontWeight: 700, borderColor: "rgba(148,163,184,0.08)", fontSize: "0.8rem" }}>
                  Weighted
                </TableCell>
                <TableCell sx={{ color: "#64748b", fontWeight: 700, borderColor: "rgba(148,163,184,0.08)", fontSize: "0.8rem" }}>
                  Date
                </TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {ranked.map((entry) => {
                const isUser = entry.username === "You";
                return (
                  <TableRow
                    key={`${entry.username}-${entry.date}-${entry.topicTitle}`}
                    sx={{
                      backgroundColor: isUser ? "rgba(16, 185, 129, 0.05)" : "transparent",
                      "&:hover": { backgroundColor: "rgba(148, 163, 184, 0.04)" },
                    }}
                  >
                    <TableCell sx={{ borderColor: "rgba(148,163,184,0.06)", py: 1.5 }}>
                      <Box sx={{ display: "flex", alignItems: "center", gap: 0.5 }}>
                        {getRankIcon(entry.rank)}
                        <Typography variant="body2" sx={{ color: getRankColor(entry.rank), fontWeight: 700 }}>
                          #{entry.rank}
                        </Typography>
                      </Box>
                    </TableCell>
                    <TableCell sx={{ borderColor: "rgba(148,163,184,0.06)", py: 1.5 }}>
                      <Box sx={{ display: "flex", alignItems: "center", gap: 1 }}>
                        <Avatar
                          sx={{
                            width: 28,
                            height: 28,
                            fontSize: "0.75rem",
                            backgroundColor: isUser ? "rgba(16,185,129,0.2)" : "rgba(148,163,184,0.1)",
                            color: isUser ? "#10b981" : "#94a3b8",
                          }}
                        >
                          {entry.username[0]}
                        </Avatar>
                        <Typography variant="body2" sx={{ color: isUser ? "#10b981" : "#e2e8f0", fontWeight: isUser ? 700 : 500 }}>
                          {entry.username}
                        </Typography>
                      </Box>
                    </TableCell>
                    <TableCell sx={{ borderColor: "rgba(148,163,184,0.06)", py: 1.5 }}>
                      <Chip
                        label={entry.topicTitle}
                        size="small"
                        sx={{
                          backgroundColor: `${entry.topicColor}15`,
                          color: entry.topicColor,
                          fontSize: "0.7rem",
                          height: 22,
                        }}
                      />
                    </TableCell>
                    <TableCell sx={{ borderColor: "rgba(148,163,184,0.06)", py: 1.5 }}>
                      <Typography variant="body2" sx={{ color: "#e2e8f0" }}>
                        {entry.score}/{entry.totalQuestions}
                      </Typography>
                    </TableCell>
                    <TableCell sx={{ borderColor: "rgba(148,163,184,0.06)", py: 1.5 }}>
                      <Typography variant="body2" sx={{ color: getGradeColor(entry.percentage), fontWeight: 700 }}>
                        {entry.percentage}%
                      </Typography>
                    </TableCell>
                    <TableCell sx={{ borderColor: "rgba(148,163,184,0.06)", py: 1.5 }}>
                      <Typography variant="body2" sx={{ color: getGradeColor(entry.weightedPercentage), fontWeight: 600 }}>
                        {entry.weightedPercentage}%
                      </Typography>
                    </TableCell>
                    <TableCell sx={{ borderColor: "rgba(148,163,184,0.06)", py: 1.5 }}>
                      <Typography variant="caption" sx={{ color: "#64748b" }}>
                        {entry.date}
                      </Typography>
                    </TableCell>
                  </TableRow>
                );
              })}
              {ranked.length === 0 && (
                <TableRow>
                  <TableCell colSpan={7} sx={{ textAlign: "center", py: 4, borderColor: "rgba(148,163,184,0.06)" }}>
                    <Typography variant="body2" sx={{ color: "#64748b" }}>
                      No results match the selected filters.
                    </Typography>
                  </TableCell>
                </TableRow>
              )}
            </TableBody>
          </Table>
        </TableContainer>
      </Card>
    </Box>
  );
}
