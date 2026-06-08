using SkyRoute.Api.Flights.Models;

namespace SkyRoute.Api.Pricing.Interfaces
{
    public interface IPricingService
    {
        Flight ApplyPricing(Flight flight, int passengers);
    }
}
