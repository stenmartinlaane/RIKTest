"use client";

import { useEffect, useState } from "react";
import { createContext } from "react";
import Event from "@/entities/Event";
import PaymentMethod from "@/entities/PaymentMethod";
import { EventProvider } from "./EventContext";
import LoginResponse from "@/entities/LoginResponse";

export default function StateComponent({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  const [events, setEvents] = useState(Array<Event>);
  const [paymentMethods, setPaymentMethods] = useState(Array<PaymentMethod>);
  const [userInfo, setUserInfo] = useState<IUserInfo | null>(null);
  const [jwtCookieExpireTimeInMinutes, setJwtCookieExpireTimeInMinutes] = useState(5);

  useEffect(() => {
    const fetchPaymentMethods = async () => {
      try {
        const res = await fetch(
          `${process.env.NEXT_PUBLIC_BACKEND_SERVER}/api/v1/PaymentMethod`, {credentials: 'include'}
        );

        if (res.status === 200) {
          const data = await res.json();
          setPaymentMethods(data);
        }
      } catch (error) {
        console.log(error);
      }
    };

    fetchPaymentMethods();
  }, []);

  const fetchJwtCookie = async () => {
    try {
      const response = await fetch(`${process.env.NEXT_PUBLIC_BACKEND_SERVER}/api/v1/identity/Account/RefreshJwt`, {
        method: 'GET',
        headers: {
          "Accept": "application/json",
        },
        credentials: 'include',
      });
      if (response.ok) {
        let loginResponse = await response.json()
        setJwtCookieExpireTimeInMinutes(Number(loginResponse.jwtCookieExpireTimeInMinutes));
      } else {
        console.error('Failed to fetch JWT token');
      }
    } catch (error) {
      console.error('Error fetching JWT token:', error);
    }
  };

  useEffect(() => {
    fetchJwtCookie();
    const intervalId = setInterval(fetchJwtCookie, jwtCookieExpireTimeInMinutes * 60 * 1000 - 30 * 1000);
    return () => clearInterval(intervalId);
  }, [jwtCookieExpireTimeInMinutes])

  return (
    <AppContext.Provider
      value={{ events, setEvents, paymentMethods, setPaymentMethods, userInfo, setUserInfo }}
    >
      <EventProvider>
        {children}
      </EventProvider>
    </AppContext.Provider>
  );
}

export interface IUserInfo {
  "jwt": string,
  "refreshToken": string,
  "email": string
}

interface IAppContext {
  events: Event[];
  setEvents: (val: Event[]) => void;
  paymentMethods: PaymentMethod[];
  setPaymentMethods: (val: PaymentMethod[]) => void;
  userInfo: IUserInfo | null,
  setUserInfo: (userInfo: IUserInfo | null) => void
}

export const AppContext = createContext<IAppContext>({
  events: [],
  setEvents: () => {},
  paymentMethods: [],
  setPaymentMethods: () => {},
  userInfo: null,
  setUserInfo: () => {}
});
