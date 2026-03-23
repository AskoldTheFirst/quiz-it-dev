"use client";

import { Inter, JetBrains_Mono } from "next/font/google";
import { Provider } from "react-redux";
import ThemeRegistry from "@/components/ThemeRegistry";
import "./globals.css";
import { store } from "@/redux/store";
import { initState } from "@/redux/appSlice";
import { current } from "@/redux/testSlice";

// Initialize store immediately (outside component for earliest execution)
if (typeof window !== "undefined") {
  store.dispatch(initState());
  store.dispatch(current());
}

const inter = Inter({ subsets: ["latin"], variable: "--font-inter" });
const jetbrainsMono = JetBrains_Mono({
  subsets: ["latin"],
  variable: "--font-mono",
});

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en" className={`${inter.variable} ${jetbrainsMono.variable}`}>
      <head>
        <title>Quiz-IT - Test Your Knowledge</title>
        <meta
          name="description"
          content="Test your knowledge across programming languages, frameworks, databases, and more."
        />
        <meta name="theme-color" content="#0f172a" />
        <link
          rel="icon"
          href="/icon-light-32x32.png"
          media="(prefers-color-scheme: light)"
        />
        <link
          rel="icon"
          href="/icon-dark-32x32.png"
          media="(prefers-color-scheme: dark)"
        />
        <link rel="icon" href="/icon.svg" type="image/svg+xml" />
        <link rel="apple-touch-icon" href="/apple-icon.png" />
      </head>
      <body className="font-sans antialiased">
        <Provider store={store}>
          <ThemeRegistry>{children}</ThemeRegistry>
        </Provider>
      </body>
    </html>
  );
}
