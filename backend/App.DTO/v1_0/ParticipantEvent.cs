using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace App.DTO.v1_0;

public class ParticipantEvent
{
    public DateTime RegisterDateTime { get; set; }

    public Guid? PersonId { get; set; }
    public Person? Person { get; set; }

    public Guid? FirmId { get; set; }
    public Firm? Firm { get; set; }

    public Guid? EventId { get; set; }
    [JsonIgnore] public Event? Event { get; set; }

    [MaxLength(5000)]
    public string AdditionalNotes { get; set; } = default!;

    public Guid? PaymentMethodId { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
}
