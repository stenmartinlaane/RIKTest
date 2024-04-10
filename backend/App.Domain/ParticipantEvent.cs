using Base.Domain;

namespace App.Domain;

public class ParticipantEvent : BaseEntityIdMetadata
{
    public DateTime RegisterDateTime { get; set; }
    
    public Guid? PersonId { get; set; }
    public Person? Person { get; set; }
    
    public Guid? FirmId { get; set; }
    public Firm? Firm { get; set; }
}