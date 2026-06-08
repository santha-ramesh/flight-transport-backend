using SkyRoute.Api.Flights.Contracts;
using SkyRoute.Api.Flights.Models;

namespace SkyRoute.Api.Providers.Interfaces
{
    public interface IAirlineProvider
    {
        string ProviderName { get; }

        Task<List<Flight>> SearchFlightsAsync(SearchRequest request);
    }
}
