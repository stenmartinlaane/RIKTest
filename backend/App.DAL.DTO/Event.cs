using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.DAL.DTO;

public class Event : BaseEntityId
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    public DateTime StartTime { get; set; }
    
    [MaxLength(256)]
    public string Location { get; set; } = default!;
    
    [MaxLength(256)]
    public string Name { get; set; } = default!;
    
    [MaxLength(1000)]
    public string AdditionalInformation { get; set; } = default!;
    
    public ICollection<ParticipantEvent>? ParticipantEvents { get; set; }
}