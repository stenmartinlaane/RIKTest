import React, {
  SetStateAction,
  createContext,
  useContext,
  useEffect,
  useState,
  Dispatch,
} from "react";
import { useParams } from "next/navigation";
import Event from "@/entities/Event";
import Spinner from "./Spinner";
import formatDate from "@/app/utils/dateFormater";
import ParticipantEvent from "@/entities/ParticipantEvent";
import ParticipantRow from "./ParticipantRow";
import AddPersonForm from "./person/AddPersonForm";
import AddFirmForm from "./firm/AddFirmForm";
import { useEventContext } from "@/context/EventContext";

const Participants = () => {
  const [loading, setLoading] = useState(true);
  const {event, setEvent} = useEventContext()!;

  const { id } = useParams() as { id: string };
  const [participantType, setParticipantType] = useState("Person");

  useEffect(() => {
    const fetchEvent = async (): Promise<Event | null> => {
      try {
        console.log(
          `${process.env.NEXT_PUBLIC_BACKEND_SERVER}/api/v1.0/Event/${id}`
        );
        const res = await fetch(
          `${process.env.NEXT_PUBLIC_BACKEND_SERVER}/api/v1.0/Event/${id}`
        );

        if (res.status === 200) {
          const data = await res.json();
          setEvent(data);
        }
      } catch (error) {
      } finally {
        setLoading(false);
      }
      return null;
    };
    if (event.id !== id) {
          fetchEvent();
    } else {
      setLoading(false);
    }
  }, [id]);

  useEffect(() => {
    console.log(event)
  }, [event])

  const handleParticipantTypeChange = (event: any) => {
    setParticipantType(event.target.value);
  };

  return (
    <EventContext.Provider value={{ event, setEvent }}>
      <div className="w-4/5 h-auto p-8">
        {loading && <Spinner loading={loading} />}
        {!loading && event && (
          <div className="flex w-full flex-col">
            <div className="w-full">
              <h2 className="pb-4 w-full">Osavõtjad</h2>
            </div>
            <div className="w-full flex">
              <div className="w-1/3">Ürituse nimi:</div>
              <div className="w-2/3">{event ? event.name : ""}</div>
            </div>
            <div className="w-full flex">
              <div className="w-1/3">Toimumisaeg:</div>
              <div className="w-2/3">
                {event ? formatDate(event.startTime) : ""}
              </div>
            </div>
            <div className="w-full flex">
              <div className="w-1/3">Koht:</div>
              <div className="w-2/3">{event ? event.location : ""}</div>
            </div>
            <div className="w-full flex">
              <div className="w-1/3">Osavõtjad:</div>
              <div className="w-2/3">
                {event.participantEvents.map(
                  (participantEvent: ParticipantEvent, index: number) => (
                    <ParticipantRow
                      key={index}
                      participantEvent={participantEvent}
                      index={index}
                    ></ParticipantRow>
                  )
                )}
              </div>
            </div>
          </div>
        )}

        <div className="h-12"></div>

        <h2>Osavõtjate lisamine</h2>

        <div className="w-100 flex my-3">
          <div className="w-1/4"></div>
          <div className="w-3/4 flex">
            <div className="w-1/2 flex">
              <div className="pr-2">
                <input
                  value={"Person"}
                  type="radio"
                  id="participantType1"
                  name="participantType1"
                  onChange={handleParticipantTypeChange}
                  checked={participantType === "Person"}
                ></input>
              </div>
              <div>
                <label htmlFor="participantType1">Eraisik</label>
              </div>
            </div>
            <div className="w-1/2 flex">
              <div className="pr-2">
                <input
                  value={"Firm"}
                  type="radio"
                  id="participantType2"
                  name="participantType2"
                  onChange={handleParticipantTypeChange}
                  checked={participantType === "Firm"}
                ></input>
              </div>
              <div>
                <label htmlFor="participantType2">Ettevõte</label>
              </div>
            </div>
          </div>
        </div>
        {participantType === "Person" ? (
          <AddPersonForm id={id} />
        ) : (
          <AddFirmForm id={id} />
        )}
      </div>
    </EventContext.Provider>
  );
};

export default Participants;

export const EventContext = createContext<{
  event: Event;
  setEvent: Dispatch<SetStateAction<Event>>;
}>({
  event: new Event(),
  setEvent: () => {},
});
