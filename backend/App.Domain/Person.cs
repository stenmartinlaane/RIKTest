using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Person : BaseEntityIdMetadata
{
    [MaxLength(256)]
    public string FirstName { get; set; } = default!;
    
    [MaxLength(256)]
    public string LastName { get; set; } = default!;
    public int PersonalIdentificationNumber { get; set; }
    
    [MaxLength(1500)]
    public string AdditionalNotes { get; set; } = default!;
    
    public ICollection<ParticipantEvent>? ParticipantEvents { get; set; }
}