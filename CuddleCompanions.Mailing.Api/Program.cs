using CuddleCompanions.Mailing.Api.Consumers;
using CuddleCompanions.Mailing.Api.Interfaces;
using CuddleCompanions.Mailing.Api.Services;
using MassTransit;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.AddConsumer<AdopterCreatedIntegrationEventConsumer>();

    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(new Uri(builder.Configuration["MessageBroker:Host"]!), h =>
        {
            h.Username(builder.Configuration["MessageBroker:Username"]);
            h.Password(builder.Configuration["MessageBroker:Password"]);
        });

        configurator.ConfigureEndpoints(context);
    });
});

builder.Services
    .AddFluentEmail(builder.Configuration["EmailSettings:DefaultFromEmail"])
    .AddSmtpSender(new SmtpClient
    {
        Host = builder.Configuration["EmailSettings:SmtpSettings:Server"],
        Port = Convert.ToInt32(builder.Configuration["EmailSettings:SmtpSettings:Port"]),
        EnableSsl = true,        
        DeliveryMethod = SmtpDeliveryMethod.Network,
        UseDefaultCredentials = false,
        Credentials = new System.Net.NetworkCredential(
            builder.Configuration["EmailSettings:SmtpSettings:Username"],
            builder.Configuration["EmailSettings:SmtpSettings:Password"])
    });

builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
