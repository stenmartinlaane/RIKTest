import React, { useState } from "react";
import Event from "@/entities/Event";
import { useRouter } from "next/navigation";
import { convertToISO8601 } from "@/utils/dateFormater";
import { toast } from "react-toastify";
import Link from "next/link";

function AddEventForm() {
  const router = useRouter();
  const [eventName, setEventName] = useState("");
  const [eventTime, setEventTime] = useState("");
  const [place, setPlace] = useState("");
  const [additionalInfo, setAdditionalInfo] = useState("");

  const handleSubmit = async (e: any) => {
    e.preventDefault();

    const data = new Event(
      "00000000-0000-0000-0000-000000000000",
      convertToISO8601(eventTime),
      place,
      eventName,
      additionalInfo,
      []
    );
    try {
      const res = await fetch(
        `${process.env.NEXT_PUBLIC_BACKEND_SERVER}/api/v1.0/Event`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(data),
          credentials: 'include',
        }
      );
      if (res.status === 201) {
        toast.success("sucess");
        router.push("/");
      } else if (res.status === 400 || res.status === 401) {
        const dataObj = await res.json();
        toast.error(`${process.env.NEXT_PUBLIC_BACKEND_SERVER}/api/v1.0/Event`);
      } else {
        toast.error(`${process.env.NEXT_PUBLIC_BACKEND_SERVER}/api/v1.0/Event`);
      }
    } catch (error) {
      toast.error("error");
    } finally {
    }
  };

  return (
    <div className="w-4/5 h-auto p-8">
      <h2>Ürituse lisamine</h2>
      <form className="w-1/2 py-2" onSubmit={handleSubmit}>
        <div className="w-100 flex my-3">
          <div className="w-1/4">
            <label htmlFor="eventName">Ürituse nimi:</label>
          </div>
          <div className="w-3/4">
            <input
              className="w-full border border-black rounded px-2"
              type="text"
              id="eventName"
              name="eventName"
              value={eventName}
              onChange={(e) => setEventName(e.target.value)}
            ></input>
          </div>
        </div>

        <div className="w-100 flex my-3">
          <div className="w-1/4">
            <label htmlFor="eventTime">Toimumisaeg:</label>
          </div>
          <div className="w-3/4">
            <input
              className="w-full border border-black rounded px-2 italic"
              type="text"
              id="eventTime"
              name="eventTime"
              placeholder="pp.kk.aaaa hh:mm"
              value={eventTime}
              onChange={(e) => setEventTime(e.target.value)}
            ></input>
          </div>
        </div>

        <div className="w-100 flex my-3">
          <div className="w-1/4">
            <label htmlFor="place">Koht:</label>
          </div>
          <div className="w-3/4">
            <input
              className="w-full border border-black rounded px-2"
              type="text"
              id="place"
              name="place"
              value={place}
              onChange={(e) => setPlace(e.target.value)}
            ></input>
          </div>
        </div>

        <div className="w-100 flex my-3">
          <div className="w-1/4">
            <label htmlFor="additionalInfo">Lisainfo:</label>
          </div>
          <div className="w-3/4 h-14">
            <textarea
              className="w-full border border-black rounded px-2 h-14"
              id="additionalInfo"
              name="additionalInfo"
              value={additionalInfo}
              onChange={(e) => setAdditionalInfo(e.target.value)}
            ></textarea>
          </div>
        </div>

        <div className="flex space-x-4 pt-8">
        <Link href="/">
            <div className="bg-secondary p-2 rounded">
              <button>Tagasi</button>
            </div>
          </Link>
          <div className="bg-primary p-2 rounded">
            <button type="submit">Lisa</button>
          </div>
        </div>
      </form>
    </div>
  );
}

export default AddEventForm;
