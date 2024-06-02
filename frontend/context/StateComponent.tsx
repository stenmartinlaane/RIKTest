"use client";

import { useEffect, useState } from "react";
import { createContext } from "react";
import Event from "@/entities/Event";
import PaymentMethod from "@/entities/PaymentMethod";
import { EventProvider } from "./EventContext";

export default function StateComponent({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  const [events, setEvents] = useState(Array<Event>);
  const [paymentMethods, setPaymentMethods] = useState(Array<PaymentMethod>);
  const [userInfo, setUserInfo] = useState<IUserInfo | null>(null);

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
