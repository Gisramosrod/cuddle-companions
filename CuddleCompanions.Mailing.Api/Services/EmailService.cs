using CuddleCompanions.Mailing.Api.Interfaces;
using FluentEmail.Core;

namespace CuddleCompanions.Mailing.Api.Services;

public sealed class EmailService : IEmailService
{
    private readonly IFluentEmail _fluentEmail;

    public EmailService(IFluentEmail fluentEmail) => _fluentEmail = fluentEmail;

    public async Task<bool> SendEmailAsync(string to, string subject, string body)
    {
        var response = await _fluentEmail
            .To(to)
            .Subject(subject)
            .Body(body, isHtml: true).SendAsync();

        return response.Successful;
    }
}
