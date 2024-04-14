using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class PaymentMethod : BaseEntityIdMetadata
{
    [MaxLength(256)]
    public string MethodName { get; set; } = default!;
    
    [MaxLength(1000)]
    public string MethodDescription { get; set; } = default!;
    public bool Active { get; set; }
    
    public ICollection<ParticipantEvent>? Firms { get; set; }
}