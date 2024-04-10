using Base.Domain;

namespace App.Domain;

public class PaymentMethod : BaseEntityIdMetadata
{
    public string MethodName { get; set; } = default!;
    public string MethodDescription { get; set; } = default!;
    public bool Active { get; set; } = false;
    
    public List<Firm>? Firms { get; set; }
    public List<Person>? Persons { get; set; }
}