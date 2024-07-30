using Contracts;
using CuddleCompanions.Mailing.Api.Interfaces;
using MassTransit;

namespace CuddleCompanions.Mailing.Api.Consumers;

public class AdopterCreatedIntegrationEventConsumer : IConsumer<AdopterCreatedIntegrationEvent>
{
    private readonly IEmailService _emailService;

    public AdopterCreatedIntegrationEventConsumer(IEmailService emailService) => _emailService = emailService;

    public async Task Consume(ConsumeContext<AdopterCreatedIntegrationEvent> context)
    {
        var message = context.Message;

        var to = message.Email;
        var subject = "Welcome to Cuddle Companions!";
        var body = $"<h1>Hello {message.FirstName} {message.LastName}</h1><p>Welcome to Cuddle Companions!</p>";

        await _emailService.SendEmailAsync(to, subject, body);
    }
}
