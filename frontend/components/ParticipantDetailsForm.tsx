import React, { useEffect, useState } from "react";
import PersonDetail from "./PersonDetail";
import FirmDetail from "./FirmDetail";
import { useParams } from "next/navigation";
import Spinner from "./Spinner";
import ParticipantEvent from "@/entities/ParticipantEvent";

function ParticipantDetailsForm() {
  const [participantEvent, setParticipantEvent] = useState<{
    participantEvent: ParticipantEvent | null;
  }>({
    participantEvent: null,
  });
  const { id } = useParams() as { id: string };
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchEvent = async (): Promise<Event | null> => {
      try {
        const res = await fetch(
          `${process.env.NEXT_PUBLIC_BACKEND_SERVER}/api/v1.0/participantEvent/${id}`
        );
        if (res.status === 200) {
          const data = await res.json();
          setParticipantEvent(data);
          console.log(data);
        }
      } catch (error) {
      } finally {
        setLoading(false);
      }
      return null;
    };
    fetchEvent();
  }, [id]);
  if (loading || participantEvent === null) {
    return <Spinner loading={loading} />;
  } else {
    <>this</>;
  }
}

export default ParticipantDetailsForm;
