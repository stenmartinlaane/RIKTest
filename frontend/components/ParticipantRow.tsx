import ParticipantEvent from "@/entities/ParticipantEvent";
import Link from "next/link";
import React, { useContext } from "react";
import { useRouter } from "next/navigation";

const ParticipantRow = ({
  participantEvent,
  index,
}: {
  participantEvent: ParticipantEvent;
  index: number;
}) => {
  const router = useRouter();
  const handleDeleteEvent = async (e: any) => {
    e.preventDefault();
    const confirmed = window.confirm(
      "Are you sure you want to remove this participant from this event?"
    );

    if (!confirmed) return;

    try {
      const res = await fetch(
        `${process.env.NEXT_PUBLIC_BACKEND_SERVER}/api/v1.0/participantEvent/${participantEvent.id}`,
        {
          method: "DELETE",
        }
      );
      if (res.status === 204) {
        window.location.reload();
      } else {
      }
    } catch (error) {
      console.log(error);
    }
  };

  if (participantEvent.person !== null) {
    const participant = participantEvent.person;
    return (
      <div className="flex">
        <div className="w-4/12">
          <p>
            {index + 1}. {participant.firstName + " " + participant.lastName}
          </p>
        </div>
        <div className="w-4/12 flex content-center justify-center h-full">
          <p className="inline-block">
            {participant.personalIdentificationNumber}
          </p>
        </div>
        <Link
          className="flex h-full w-2/12 hover:border-primary hover:rounded hover:border-2 cursor-pointer"
          href={`/osalejad-detail/` + participantEvent.id}
        >
          <div className="inline-block w-full pl-1">VAATA</div>
        </Link>
        <div
          className="w-2/12 flex content-center justify-center h-full hover:border-primary hover:rounded hover:border-2 cursor-pointer"
          onClick={handleDeleteEvent}
        >
          <div className="inline-block w-full pl-1">KUSTUTA</div>
        </div>
      </div>
    );
  } else if (participantEvent.firm !== null) {
    const participant = participantEvent.firm;
    return (
      <div className="flex">
        <div className="w-4/12">
          <p>
            {index + 1}. {participant.name}
          </p>
        </div>
        <div className="w-4/12 flex content-center justify-center h-full">
          <p className="inline-block">{participant.registryCode}</p>
        </div>
        <Link
          className="flex content-center justify-center h-full  w-2/12 hover:border-primary hover:rounded hover:border-2 cursor-pointer"
          href={`/osalejad-detail/` + participantEvent.id}
        >
          <div className="inline-block w-full pl-1">VAATA</div>
        </Link>
        <div
          className="w-2/12 flex content-center justify-center h-full hover:border-primary hover:rounded hover:border-2 cursor-pointer"
          onClick={handleDeleteEvent}
        >
          <div className="inline-block w-full pl-1">KUSTUTA</div>
        </div>
      </div>
    );
  } else {
    <></>;
  }
};

export default ParticipantRow;
