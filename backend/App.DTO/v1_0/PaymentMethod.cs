using System.ComponentModel.DataAnnotations;


namespace App.DTO.v1_0;

public class PaymentMethod
{
    [MaxLength(256)]
    public string MethodName { get; set; } = default!;
    
    [MaxLength(256)]
    public string MethodDescription { get; set; } = default!;
    public bool Active { get; set; }
    
    public ICollection<ParticipantEvent>? ParticipantEvents { get; set; }
}