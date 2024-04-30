"use client";
import Event from "@/entities/Event";
import { createContext, useContext, useState } from "react";

type EventContextType = {
    event: Event;
    setEvent: React.Dispatch<React.SetStateAction<Event>>;
  };
  
  const EventContext = createContext<EventContextType | null>(null);

export function EventProvider({
    children,
  }: Readonly<{
    children: React.ReactNode;
  }>) {
  const [event, setEvent] = useState(new Event);

  return (
    <EventContext.Provider
      value={{
        event,
        setEvent,
      }}
    >
      {children}
    </EventContext.Provider>
  );
}

export function useEventContext() {
  return useContext(EventContext);
}
