using Contracts;
using CuddleCompanions.Domain.Events;
using MassTransit;
using MediatR;

namespace CuddleCompanions.Application.Adopters.Events;

internal sealed class AdopterCreatedEventHandler : INotificationHandler<AdopterCreatedEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public AdopterCreatedEventHandler(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task Handle(AdopterCreatedEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new AdopterCreatedIntegrationEvent(
            notification.Email.Value,
            notification.FirstName,
            notification.LastName);

        await _publishEndpoint.Publish(integrationEvent, cancellationToken);
    }
}
