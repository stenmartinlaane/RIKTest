using Base.Domain;

namespace App.DAL.DTO;

public class Person : BaseEntityId
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public int PersonalIdentificationNumber { get; set; }
    public string AdditionalNotes { get; set; } = default!;
    
    public List<ParticipantEvent>? ParticipantEvents { get; set; }
}