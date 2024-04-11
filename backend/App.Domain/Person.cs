using Base.Domain;

namespace App.Domain;

public class Person : BaseEntityIdMetadata
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public int PersonalIdentificationNumber { get; set; }
    public string AdditionalNotes { get; set; } = default!;
    
    public List<ParticipantEvent>? ParticipantEvents { get; set; }
}