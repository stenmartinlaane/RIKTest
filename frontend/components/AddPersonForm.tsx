import Person from "@/entities/Person";
import Event from "@/entities/Event";
import React, { useContext, useEffect, useState } from "react";
import { AppContext } from "../context/StateComponent";
import ParticipantEvent from "@/entities/ParticipantEvent";
import PaymentMethod from "@/entities/PaymentMethod";
import Link from "next/link";
import { useEventContext } from "@/context/EventContext";
import { toast } from "react-toastify";

const AddPersonForm = ({ id }: { id: string }) => {
  const [firstName, setFirstName] = useState("");
  const [lastName, setEventTime] = useState("");
  const [personalIdentificationNumber, setPersonalIdentificationNumber] =
    useState("");
  const { paymentMethods } = useContext(AppContext);
  const [paymentMethodId, setPaymentMethodId] = useState("");
  const [additionalInfo, setAdditionalInfo] = useState("");
  const {event, setEvent} = useEventContext()!;

  useEffect(() => {
    const pmId = paymentMethods.filter((pm) => pm.active)[0]?.id ?? "";
    setPaymentMethodId(pmId);
  }, [paymentMethods]);

  const handleSubmit = async (e: any) => {
    e.preventDefault();

    const person = new Person(
      "00000000-0000-0000-0000-000000000000",
      firstName,
      lastName,
      Number(personalIdentificationNumber),
      additionalInfo,
      []
    );

    const data = new ParticipantEvent(
      "00000000-0000-0000-0000-000000000000",
      new Date().toISOString(),
      "00000000-0000-0000-0000-000000000000",
      person,
      null,
      null,
      paymentMethodId,
      null,
      id,
      null
    );

    console.log(JSON.stringify(data));
    try {
      const res = await fetch(
        `${process.env.NEXT_PUBLIC_BACKEND_SERVER}/api/v1/ParticipantEvent`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(data),
        }
      );
      console.log(res.status)
      if (res.status === 201) {
        let participantEvent = await res.json(); 
        setEvent(prevEvent => {
          const updatedEvent = { ...prevEvent };
          updatedEvent.participantEvents = [...updatedEvent.participantEvents, participantEvent as ParticipantEvent];
          return updatedEvent;
        });
        toast.success("Eraisik üritusele lisatud.")

        
      } else if (res.status === 400 || res.status === 401) {
        const dataObj = await res.json();
        console.log(dataObj);
        toast.error("Viga eraisiku üritusele lisamisel.")
      }
    } catch (error) {
      console.log(error)
    } finally {
    }
  };

  return (
    <>
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
              onChange={(e) => setPersonalIdentificationNumber(e.target.value)}
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
          <Link href="/">
            <div className="bg-secondary p-2 rounded">
              <button>Tagasi</button>
            </div>
          </Link>
          <div className="bg-primary p-2 rounded">
            <button type="submit">Salvesta</button>
          </div>
        </div>
      </form>
    </>
  );
};

export default AddPersonForm;
