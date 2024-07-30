namespace CuddleCompanions.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; init; }

    private readonly List<IDomainEvent> _domainEvents = new();

    protected Entity() { }

    protected Entity(Guid id)
    {
        Id = id;
    }

    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public List<IDomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();
        _domainEvents.Clear();

        return copy;
    } 
}
