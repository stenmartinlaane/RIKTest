import ParticipantEvent from "./ParticipantEvent";

export default class Event {
  id: string;
  startTime: string;
  location: string;
  name: string;
  additionalInformation: string;
  participantEvents: ParticipantEvent[];

  constructor(
    id?: string,
    startTime?: string,
    location?: string,
    name?: string,
    additionalInformation?: string,
    participantEvents?: ParticipantEvent[]
  ) {
    if (
      id &&
      startTime &&
      location &&
      name &&
      additionalInformation &&
      participantEvents
    ) {
      this.id = id;
      this.startTime = startTime;
      this.location = location;
      this.name = name;
      this.additionalInformation = additionalInformation;
      this.participantEvents = participantEvents;
    } else {
      this.id = "";
      this.startTime = "";
      this.location = "";
      this.name = "";
      this.additionalInformation = "";
      this.participantEvents = [];
    }
  }
}
