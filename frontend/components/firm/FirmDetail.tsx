import React, { useContext, useState } from "react";
import { AppContext } from "../../context/StateComponent";
import Firm from "@/entities/Firm";
import PaymentMethod from "@/entities/PaymentMethod";
import ParticipantEvent from "@/entities/ParticipantEvent";
import { toast } from "react-toastify";
import { useRouter } from "next/navigation";
import { useEventContext } from "@/context/EventContext";
import Link from "next/link";

const FirmDetail = ({participantEvent}: {participantEvent: ParticipantEvent}) => {
  const router = useRouter();
  const [name, setName] = useState(participantEvent.firm!.name);
  const [registryCode, setregistryCode] = useState(participantEvent.firm!.registryCode);
  const [participantCount, setparticipantCount] = useState(participantEvent.firm!.participantCount);
  const { paymentMethods } = useContext(AppContext);
  const [paymentMethodId, setPaymentMethodId] = useState(participantEvent.paymentMethodId);
  const [additionalInfo, setAdditionalInfo] = useState(participantEvent.additionalNotes);
  const {event, setEvent} = useEventContext()!;

  const handleSubmit = async (e: any) => {
    e.preventDefault();

    const firm = new Firm(
      participantEvent.firmId!,
      name,
      registryCode,
      Number(participantCount),
      []
    );

    const data = new ParticipantEvent(
      participantEvent.id,
      new Date().toISOString(),
      null,
      null,
      participantEvent.firmId,
      firm,
      paymentMethodId,
      null,
      participantEvent.eventId,
      null,
      additionalInfo
    );
    try {
      const res = await fetch(
        `${process.env.NEXT_PUBLIC_BACKEND_SERVER}/api/v1/ParticipantEvent/${participantEvent.id}`,
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(data),
        }
      );
      console.log(res.status)
      if (res.status === 201) {
        let participantEvent = data; 
        setEvent(prevEvent => {
          const updatedEvent = { ...prevEvent };
          updatedEvent.participantEvents = updatedEvent.participantEvents.filter((pe) => pe.id !== participantEvent.id);
          updatedEvent.participantEvents = [...updatedEvent.participantEvents, participantEvent as ParticipantEvent];
          return updatedEvent;
        });
        toast.success("Firma andmed on uuendatud.")
        router.push("/osalejad/" + participantEvent.eventId);
      } else if (res.status === 400 || res.status === 401) {
        const dataObj = await res.json();
        console.log(dataObj);
        toast.error("Tekkis viga eraisiku andmete uuendamisel.")
      }
    } catch (error) {
      console.log(error)
    } finally {
    }
  };

  return (
    <>
      <h2>Osavõtja info</h2>
      <form className="w-1/2 py-2" onSubmit={handleSubmit}>
        <div className="w-100 flex my-3">
          <div className="w-1/4">
            <label htmlFor="name">Ettevõte Nimi:</label>
          </div>
          <div className="w-3/4">
            <input
              className="w-full border border-black rounded px-2"
              type="text"
              id="name"
              name="name"
              value={name}
              onChange={(e) => setName(e.target.value)}
            ></input>
          </div>
        </div>

        <div className="w-100 flex my-3">
          <div className="w-1/4">
            <label htmlFor="registryCode">Registrikood:</label>
          </div>
          <div className="w-3/4">
            <input
              className="w-full border border-black rounded px-2"
              type="text"
              id="registryCode"
              name="registryCode"
              value={registryCode}
              onChange={(e) => setregistryCode(e.target.value)}
            ></input>
          </div>
        </div>

        <div className="w-100 flex my-3">
          <div className="w-1/4">
            <label htmlFor="participantCount">Osaõtjate arv:</label>
          </div>
          <div className="w-3/4">
            <input
              className="w-full border border-black rounded px-2"
              type="text"
              id="participantCount"
              name="participantCount"
              value={participantCount}
              onChange={(e) => setparticipantCount(Number(e.target.value))}
            ></input>
          </div>
        </div>

        <div className="w-100 flex my-3">
          <div className="w-1/4">
            <label htmlFor="paymentMethod">Maksmisviis:</label>
          </div>
          <div className="w-3/4">
            <select
              className="w-full border border-black rounded px-1"
              id="paymentMethod"
              name="paymentMethod"
              value={paymentMethodId}
              onChange={(e) => setPaymentMethodId(e.target.value)}
            >
              {paymentMethods
                .filter((pm) => pm.active)
                .map((pm: PaymentMethod, index: number) => (
                  <option key={index} value={pm.id}>
                    {pm.methodName}
                  </option>
                ))}
            </select>
          </div>
        </div>

        <div className="w-100 flex my-3">
          <div className="w-1/4">
            <label htmlFor="additionalInfo">Lisainfo:</label>
          </div>
          <textarea
            className="w-3/4 border border-black rounded px-2 h-14"
            id="additionalInfo"
            name="additionalInfo"
            value={additionalInfo}
            onChange={(e) => setAdditionalInfo(e.target.value)}
          ></textarea>
        </div>

        <div className="flex space-x-4">
          <Link href={"/osalejad/" + participantEvent.eventId}>
            <div className="bg-secondary p-2 rounded">
              <button>Tagasi</button>
            </div>
          </Link>
          <div className="bg-primary p-2 rounded">
            <button type="submit" className='text-white'>Salvesta</button>
          </div>
        </div>
      </form>
    </>
  );
};

export default FirmDetail;
