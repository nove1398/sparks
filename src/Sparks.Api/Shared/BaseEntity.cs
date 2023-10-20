using Postgrest.Attributes;
using Postgrest.Models;

namespace Sparks.Api.Shared;

public abstract class BaseEntity 
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAtUtc { get; protected set; }
    private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

    public IReadOnlyList<IDomainEvent> GetDomainEvents() => _domainEvents.AsReadOnly();

    protected void ClearDomainEvents() => _domainEvents.Clear();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}