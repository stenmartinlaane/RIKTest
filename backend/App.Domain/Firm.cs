using Base.Domain;

namespace App.Domain;

public class Firm : BaseEntityIdMetadata
{
    public string Name { get; set; } = default!;
    public string RegistryCode { get; set; } = default!;
    public int ParticipantCount { get; set; }
    public string AdditionalNotes { get; set; } = default!;
    
    public Guid? PaymentMethodId { get; set; }
    public PaymentMethod? PaymentMethod { get; set; } = default!;
    
    public List<ParticipantEvent>? ParticipantEvents { get; set; }
}