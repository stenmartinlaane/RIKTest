export default interface IEvent {
    id: string,
    startTime: number,
    location: string,
    name: string,
    additionalInformation: string,
    participantEvents: any[]
}