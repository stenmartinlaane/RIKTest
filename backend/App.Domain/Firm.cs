using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Base.Domain;

namespace App.Domain
{

    public class Firm : BaseEntityIdMetadata
    {
        [MaxLength(256)]
        public string Name { get; set; } = default!;

        [MaxLength(256)]
        public string RegistryCode { get; set; } = default!;
        public int ParticipantCount { get; set; }

        [JsonIgnore]
        public ICollection<ParticipantEvent>? ParticipantEvents { get; set; }
    }
}