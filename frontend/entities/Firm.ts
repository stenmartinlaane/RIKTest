import ParticipantEvent from "./ParticipantEvent";

export default class Firm {
    id: string;
    name: string;
    registryCode: string;
    participantCount: number;
    additionalNotes: string;
    participantEvents: ParticipantEvent[];

    constructor(
        id: string,
        name: string,
        registryCode: string,
        participantCount: number,
        additionalNotes: string,
        participantEvents: ParticipantEvent[] 
    ) {
        this.id = id;
        this.name = name;
        this.registryCode = registryCode;
        this.participantCount = participantCount;
        this.additionalNotes = additionalNotes;
        this.participantEvents = participantEvents 
    }
}
