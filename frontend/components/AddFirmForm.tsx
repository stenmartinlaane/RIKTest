import Firm from "@/entities/Firm";
import ParticipantEvent from "@/entities/ParticipantEvent";
import React, { useContext, useEffect, useState } from "react";
import { AppContext } from "../context/StateComponent";
import PaymentMethod from "@/entities/PaymentMethod";

const AddFirmForm = ({ id }: { id: string }) => {
  const [name, setName] = useState("");
  const [registryCode, setregistryCode] = useState("0");
  const [participantCount, setparticipantCount] = useState("0");
  const { paymentMethods } = useContext(AppContext);
  const pmId = paymentMethods.filter((pm) => pm.active)[0]?.id ?? "";
  const [paymentMethodId, setPaymentMethodId] = useState(pmId);
  const [additionalInfo, setAdditionalInfo] = useState("");

  const handleSubmit = async (e: any) => {
    e.preventDefault();

    const firm = new Firm(
      "00000000-0000-0000-0000-000000000000",
      name,
      registryCode,
      Number(participantCount),
      additionalInfo,
      []
    );

    const data = new ParticipantEvent(
      "00000000-0000-0000-0000-000000000000",
      new Date().toISOString(),
      null,
      null,
      "00000000-0000-0000-0000-000000000000",
      firm,
      paymentMethodId,
      null,
      id,
      null
    );
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
      if (res.status === 201) {
        window.location.reload();
      } else if (res.status === 400 || res.status === 401) {
        const dataObj = await res.json();
        console.log(dataObj);
      }
    } catch (error) {
    } finally {
    }
  };

  return (
    <>
      {" "}
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
              onChange={(e) => setparticipantCount(e.target.value)}
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
          <div className="bg-secondary p-2 rounded">
            <button>Tagasi</button>
          </div>

          <div className="bg-primary p-2 rounded">
            <button type="submit">Salvesta</button>
          </div>
        </div>
      </form>
    </>
  );
};

export default AddFirmForm;
