"use client";

import { useEffect } from "react";
import PersonalStatistics from "@/components/quiz/PersonalStatistics";
import { useAppDispatch } from "@/redux/store";
import { getProfile } from "@/redux/statSlice";

export default function ProfilePage() {
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(getProfile());
  }, [dispatch]);

  return <PersonalStatistics />;
}