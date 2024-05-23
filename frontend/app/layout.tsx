import Footer from "@/components/layout/Footer";
import { Metadata } from "next";
import "./globals.css";
import NavBar from "@/components/layout/NavBar";
import StateComponent from "@/context/StateComponent";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { GoogleOAuthProvider } from '@react-oauth/google';

export const metadata: Metadata = {
  title: "Next.js",
  description: "Generated by Next.js",
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <GoogleOAuthProvider clientId="254614854717-n391mjd53sdld5d9vckp1n325nhl71eu.apps.googleusercontent.com">
      <html lang="en">
        <body>
          <div className="bg-black w-full h-screen border-full flex flex-col">
            <NavBar></NavBar>
            <StateComponent>
              <main className="grow flex-1 w-full h-50 bg-background py-4">
                {children}
              </main>
            </StateComponent>
            <Footer></Footer>
          </div>
          <ToastContainer />
        </body>
      </html>
    </GoogleOAuthProvider>
  );
}
