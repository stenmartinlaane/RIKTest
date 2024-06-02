using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Base.DTO;

namespace App.DTO.v1_0;

public class Firm: BaseEntity
{
    [MaxLength(256)]
    public string Name { get; set; } = default!;
    
    [MaxLength(256)]
    public string RegistryCode { get; set; } = default!;
    public int ParticipantCount { get; set; }

    [JsonIgnore]
    public ICollection<ParticipantEvent>? ParticipantEvents { get; set; }
}