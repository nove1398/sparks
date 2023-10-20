using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Sparks.Api.Shared;

namespace Sparks.Api.Database;

public class DomainEventInterceptor : SaveChangesInterceptor
{
    private readonly IPublisher _mediator;
    public DomainEventInterceptor(IServiceProvider serviceProvider)
    {
        _mediator = serviceProvider.GetService<IPublisher>()!;
    }
    
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        DbContext? dbContext = eventData.Context;

        if (dbContext is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        List<IReadOnlyList<IDomainEvent>> domainEvents = dbContext.ChangeTracker.Entries<BaseEntity>()
            .Where(x =>
                x.State is EntityState.Added 
                    or EntityState.Modified 
                    or EntityState.Deleted)
            .Select(x => x.Entity.GetDomainEvents())
            .ToList();

        if (!domainEvents.Any())
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        foreach (var domainEvent in domainEvents)
        {
            _mediator.Publish(domainEvent, cancellationToken);
        }
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    
}