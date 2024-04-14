using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.DAL.DTO;

public class PaymentMethod : BaseEntityId
{
    [MaxLength(256)]
    public string MethodName { get; set; } = default!;
    
    [MaxLength(256)]
    public string MethodDescription { get; set; } = default!;
    public bool Active { get; set; }
    
    public ICollection<ParticipantEvent>? Firms { get; set; }
}