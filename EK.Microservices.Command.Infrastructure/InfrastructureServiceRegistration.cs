using EK.Microservices.Command.Application.Aggregates;
using EK.Microservices.Command.Infrastructure.KafkaEvents;
using EK.Microservices.Cqrs.Core.Handlers;
using EK.Microservices.Cqrs.Core.Infrastructure;
using EK.Microservices.Cqrs.Core.Producers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EK.Microservices.Command.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEventProducer, EventProducer>();
            services.AddTransient<IEventStore, EventStore>();
            services.AddTransient<IEventSourcingHandler<AccountAggregate>, EventSourcingHandler>();

            return services;
        }
    }
}
