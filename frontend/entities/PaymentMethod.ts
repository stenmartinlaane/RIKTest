import ParticipantEvent from "./ParticipantEvent";

export default class PaymentMethod {
    id: string;
    methodName: string;
    methodDescription: string;
    active: boolean;
    additionalInformation: string;
    participantEvents: ParticipantEvent[];

    constructor(
        id: string,
        methodName: string,
        methodDescription: string,
        active: boolean,
        additionalInformation: string,
        participantEvents: ParticipantEvent[]
    ) {
        this.id = id;
        this.methodName = methodName;
        this.methodDescription = methodDescription;
        this.active = active;
        this.additionalInformation = additionalInformation;
        this.participantEvents = participantEvents;
    }
}
