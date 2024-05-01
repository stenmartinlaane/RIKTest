import ParticipantEvent from "./ParticipantEvent";

export default class Person {
    id: string;
    firstName: string;
    lastName: string;
    personalIdentificationNumber: number;
    participantEvents: ParticipantEvent[];

    constructor(
        id: string,
        firstName: string,
        lastName: string,
        personalIdentificationNumber: number,
        participantEvents: ParticipantEvent[] 
    ) {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.personalIdentificationNumber = personalIdentificationNumber;
        this.participantEvents = participantEvents 
    }
}
