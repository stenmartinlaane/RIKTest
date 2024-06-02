using System.ComponentModel.DataAnnotations;
using Base.DTO;

namespace App.DTO.v1_0;

public class Event: BaseEntity
{
    public DateTime StartTime { get; set; }
    
    [MaxLength(256)]
    public string Location { get; set; } = default!;
    
    [MaxLength(256)]
    public string Name { get; set; } = default!;
    
    [MaxLength(1000)]
    public string AdditionalInformation { get; set; } = default!;
    
    public ICollection<ParticipantEvent>? ParticipantEvents { get; set; }
}