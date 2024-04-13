using Base.Domain;

namespace App.DAL.DTO;

public class PaymentMethod : BaseEntityId
{
    public string MethodName { get; set; } = default!;
    public string MethodDescription { get; set; } = default!;
    public bool Active { get; set; }
    
    public List<ParticipantEvent>? Firms { get; set; }
}