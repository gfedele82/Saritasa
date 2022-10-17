using Contracts.Engine;
using Contracts.Providers;
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Engine;
using Engine.Providers;
using Engine.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.Configuration;
using Models.Request;
using System.Diagnostics.CodeAnalysis;


namespace Broker.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static void RegisterRepository(this IServiceCollection services)
        {
            services.AddScoped<IRatesRepository, RatesRepository>();
        }

        public static void RegisterDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection(ConnectionStringSettings.KEY).Get<ConnectionStringSettings>();
            services.AddDbContext<RatesContext>(options => options.UseSqlServer(settings.DefaultConnectionString), ServiceLifetime.Transient);
        }

        public static void RegisterConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration.GetSection(ProviderSettings.KEY).Get<ProviderSettings>());
        }

        public static void RegisterValidation(this IServiceCollection services)
        {
            services.AddTransient<IValidator<RequestRates>, BrokerValidator>();
        }

        public static void RegisterEngines(this IServiceCollection services)
        {
            services.AddScoped<IBrokerEngine, BrokerEngine>();
            services.AddScoped<IRateProviderEngine, RateProviderEngine>();
        }
    }
}
