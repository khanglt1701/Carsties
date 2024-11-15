import type { Metadata } from "next";
import "./globals.css";
import NavBar from "./nav/NavBar";
import ToasterProvider from "./providers/ToasterProvider";
import SignalRProvider from "./providers/SignalRProvider";
import { getCurrentUser } from "./actions/authAction";


export const metadata: Metadata = {
  title: "Create Next App",
  description: "Generated by create next app",
};

export default async function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  const user = await getCurrentUser();

  return (
    <html lang="en">
      <body>
        <ToasterProvider />
        <NavBar />
        <main className="container mx-auto px-5 pt-10">
          <SignalRProvider user={user}>
            {children}
          </SignalRProvider>
        </main>
      </body>
    </html>
  );
}
