namespace Base.DTO;

public class BaseEntity: BaseEntity<Guid>
{
    
}

public abstract class BaseEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public TKey? Id { get; set; }
}