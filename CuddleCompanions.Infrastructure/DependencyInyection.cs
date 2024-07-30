using CuddleCompanions.Domain.Repositories;
using CuddleCompanions.Infrastructure.Common;
using CuddleCompanions.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CuddleCompanions.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        if (connectionString.IsNullOrEmpty())
        {
            throw new ArgumentNullException("The connection string is null");
        }
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();
            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(configuration["MessageBroker:Host"]!), h =>
                {
                    h.Username(configuration["MessageBroker:Username"]);
                    h.Password(configuration["MessageBroker:Password"]);
                });

                configurator.ConfigureEndpoints(context);
            });
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<IAdopterRepository, AdopterRepository>();
        services.AddScoped<IAdoptionRecordRepository, AdoptionRecordRepository>();

        return services;
    }
}