using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace App.DTO.v1_0;

public class Person
{
    [MaxLength(256)]
    public string FirstName { get; set; } = default!;
    
    [MaxLength(256)]
    public string LastName { get; set; } = default!;
    public int PersonalIdentificationNumber { get; set; }
    
    [JsonIgnore]
    public ICollection<ParticipantEvent>? ParticipantEvents { get; set; }
}