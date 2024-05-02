import { useEventContext } from '@/context/EventContext';
import { AppContext } from '@/context/StateComponent';
import ParticipantEvent from '@/entities/ParticipantEvent';
import PaymentMethod from '@/entities/PaymentMethod';
import Person from '@/entities/Person'
import Link from 'next/link';
import { useRouter } from "next/navigation";
import React, { useContext, useState } from 'react'
import { toast } from 'react-toastify';

const PersonDetail = ({participantEvent}: {participantEvent: ParticipantEvent}) => {
  const router = useRouter();
  const [firstName, setFirstName] = useState(participantEvent.person!.firstName);
  const [lastName, setEventTime] = useState(participantEvent.person!.lastName);
  const [personalIdentificationNumber, setPersonalIdentificationNumber] =
    useState(participantEvent.person!.personalIdentificationNumber);
  const { paymentMethods } = useContext(AppContext);
  const [paymentMethodId, setPaymentMethodId] = useState(participantEvent.paymentMethodId);
  const [additionalInfo, setAdditionalInfo] = useState(participantEvent.additionalNotes);
  const {event, setEvent} = useEventContext()!;

  const handleSubmit = async (e: any) => {
    e.preventDefault();
    const confirmed = window.confirm(
      "Kas sa oled kindel, et tahad isikuandmeid uuendada?"
    );
    if (!confirmed) return;

    const person = new Person(
      participantEvent.personId!,
      firstName,
      lastName,
      Number(personalIdentificationNumber),
      []
    );

    const data = new ParticipantEvent(
      participantEvent.id,
      new Date().toISOString(),
      participantEvent.personId!,
      person,
      null,
      null,
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
        toast.success("Eraisiku andmed on uuendatud.")
        router.push("/osalejad/" + participantEvent.eventId);
      } else if (res.status === 400 || res.status === 401 || res.status === 405) {
        const dataObj = await res.json();
        console.log(dataObj);
        toast.error("Tekkis viga eraisiku andmete uuendamisel.")
      }
    } catch (error) {
      console.log(error)
    } finally {
    }
  }

  return (
    <>
      <h2>Osavõtja info</h2>
      <form className="w-1/2 py-2" onSubmit={handleSubmit}>
        <div className="w-100 flex my-3">
          <div className="w-1/4">
            <label htmlFor="firstName">Eesnimi:</label>
          </div>
          <div className="w-3/4">
            <input
              className="w-full border border-black rounded px-2"
              type="text"
              id="firstName"
              name="firstName"
              value={firstName}
              onChange={(e) => setFirstName(e.target.value)}
            ></input>
          </div>
        </div>

        <div className="w-100 flex my-3">
          <div className="w-1/4">
            <label htmlFor="lastName">Perenimi:</label>
          </div>
          <div className="w-3/4">
            <input
              className="w-full border border-black rounded px-2"
              type="text"
              id="lastName"
              name="lastName"
              value={lastName}
              onChange={(e) => setEventTime(e.target.value)}
            ></input>
          </div>
        </div>

        <div className="w-100 flex my-3">
          <div className="w-1/4">
            <label htmlFor="personalIdentificationNumber">Isikukood:</label>
          </div>
          <div className="w-3/4">
            <input
              className="w-full border border-black rounded px-2"
              type="text"
              id="personalIdentificationNumber"
              name="personalIdentificationNumber"
              value={personalIdentificationNumber}
              onChange={(e) => setPersonalIdentificationNumber(Number(e.target.value))}
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
  )
}

export default PersonDetail