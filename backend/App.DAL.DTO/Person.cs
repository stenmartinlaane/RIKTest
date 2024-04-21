using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Base.Domain;

namespace App.DAL.DTO;

public class Person : BaseEntityId
{
    [MaxLength(256)]
    public string FirstName { get; set; } = default!;
    
    [MaxLength(256)]
    public string LastName { get; set; } = default!;
    public int PersonalIdentificationNumber { get; set; }
    
    [MaxLength(1500)]
    public string AdditionalNotes { get; set; } = default!;
    
    [JsonIgnore]
    public ICollection<ParticipantEvent>? ParticipantEvents { get; set; }
}