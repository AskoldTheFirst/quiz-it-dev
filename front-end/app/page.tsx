"use client";

import { useState, useEffect } from "react";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import QuizPage from "@/components/pages/QuizPage";
import TopPage from "@/components/pages/TopPage";
import ProfilePage from "@/components/pages/ProfilePage";
import AboutPage from "@/components/pages/AboutPage";
import AppLayout from "@/components/layout/AppLayout";

const routes = [
  {
    path: "/",
    element: <AppLayout />,
    children: [
      {
        index: true,
        element: <QuizPage />,
      },
      {
        path: "top",
        element: <TopPage />,
      },
      {
        path: "profile",
        element: <ProfilePage />,
      },
      {
        path: "about",
        element: <AboutPage />,
      },
    ],
  },
];

export default function CatchAllPage() {
  const [router, setRouter] = useState<ReturnType<typeof createBrowserRouter> | null>(null);

  useEffect(() => {
    setRouter(createBrowserRouter(routes));
  }, []);

  if (!router) {
    return null;
  }

  return <RouterProvider router={router} />;
}
