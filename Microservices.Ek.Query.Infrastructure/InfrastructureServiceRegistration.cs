using Microservices.Ek.Query.Domain;
using Microservices.Ek.Query.Infrastructure.Persistence;
using Microservices.Ek_Query_Application.Contracts.Persistence;
using Microservices.Ek_Query_Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.Ek.Query.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<EmailSettings>(configuration.GetSection("drivers:notifications:email"));

            services.AddScoped<IAsyncServiceEmail, AsyncEmailService>();
            return services;
        }
    }
}
