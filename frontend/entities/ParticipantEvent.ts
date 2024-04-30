import Firm from "./Firm";
import PaymentMethod from "./PaymentMethod";
import Person from "./Person";
import Event from "./Event";


export default class ParticipantEvent {
    id: string;
    registerDateTime: string;
    personId: string | null;
    person: Person | null;
    firmId: string | null;
    firm: Firm | null;
    paymentMethodId: string;
    paymentMethod: PaymentMethod | null;
    eventId: string;
    event: Event | null;

    constructor(
        id: string,
        registerDateTime: string,
        personId: string | null,
        person: Person | null,
        firmId: string | null,
        firm: Firm | null,
        paymentMethodId: string,
        paymentMethod: PaymentMethod | null,
        eventId: string,
        event: Event | null
    ) {
        this.id = id;
        this.registerDateTime = registerDateTime;
        this.personId = personId;
        this.person = person;
        this.firmId = firmId;
        this.firm = firm;
        this.paymentMethodId = paymentMethodId;
        this.paymentMethod = paymentMethod;
        this.eventId = eventId
        this.event = event
    }
}
