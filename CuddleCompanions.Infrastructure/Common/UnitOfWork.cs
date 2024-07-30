using CuddleCompanions.Domain.Common;
using CuddleCompanions.Domain.Repositories;
using MediatR;

namespace CuddleCompanions.Infrastructure.Common;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    private readonly IPublisher _publisher;

    public UnitOfWork(AppDbContext dbContext, IPublisher publisher)
    {
        _dbContext = dbContext;
        _publisher = publisher;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = _dbContext.ChangeTracker
            .Entries<Entity>()
            .SelectMany(entry => entry.Entity.PopDomainEvents())
            .ToList();

        await _dbContext.SaveChangesAsync(cancellationToken);

        await PublishDomainEvents(domainEvents);
    }

    private async Task PublishDomainEvents(List<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }
}
