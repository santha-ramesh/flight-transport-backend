using SkyRoute.Api.Flights.Services;
using SkyRoute.Api.Flights.Services.Interfaces;
using SkyRoute.Api.Pricing;
using SkyRoute.Api.Pricing.Interfaces;
using SkyRoute.Api.Providers;
using SkyRoute.Api.Providers.Interfaces;

namespace SkyRoute.Api.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IPricingService, PricingService>();
            services.AddTransient<IAirlineProvider, BudgetWingsProvider>();
            services.AddTransient<IAirlineProvider, GlobalAirProvider>();
            services.AddTransient<IFlightSearchService, FlightSearchService>();

            return services;
        }
    }
}
