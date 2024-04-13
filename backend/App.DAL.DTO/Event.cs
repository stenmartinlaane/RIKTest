using Base.Domain;

namespace App.DAL.DTO;

public class Event : BaseEntityId
{
    public DateTime StartTime { get; set; }
    public string Location { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string AdditionalInformation { get; set; } = default!;
    
    public List<ParticipantEvent>? ParticipantEvents { get; set; }
}