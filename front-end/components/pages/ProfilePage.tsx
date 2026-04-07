"use client";

import { useEffect } from "react";
import Statistics from "@/components/quiz/Statistics";
import { useAppDispatch } from "@/redux/store";
import { getProfile } from "@/redux/statSlice";

export default function ProfilePage() {
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(getProfile());
  }, [dispatch]);

  return <Statistics />;
}