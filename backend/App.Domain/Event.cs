using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Event : BaseEntityIdMetadata
{
    public DateTime StartTime { get; set; }
    public string Location { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string AdditionalInformation { get; set; } = default!;
    
    public ICollection<ParticipantEvent>? ParticipantEvents { get; set; }
}