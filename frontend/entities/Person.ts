import ParticipantEvent from "./ParticipantEvent";

export default class Person {
    id: string;
    firstName: string;
    lastName: string;
    personalIdentificationNumber: number;
    additionalNotes: string;
    participantEvents: ParticipantEvent[];

    constructor(
        id: string,
        firstName: string,
        lastName: string,
        personalIdentificationNumber: number,
        additionalNotes: string,
        participantEvents: ParticipantEvent[] 
    ) {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.personalIdentificationNumber = personalIdentificationNumber;
        this.additionalNotes = additionalNotes;
        this.participantEvents = participantEvents 
    }
}
