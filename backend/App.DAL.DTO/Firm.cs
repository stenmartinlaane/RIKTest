using Base.Domain;

namespace App.DAL.DTO;

public class Firm : BaseEntityId
{
    public string Name { get; set; } = default!;
    public string RegistryCode { get; set; } = default!;
    public int ParticipantCount { get; set; }
    public string AdditionalNotes { get; set; } = default!;
    

    
    public List<ParticipantEvent>? ParticipantEvents { get; set; }
}