using SkyRoute.Api.Flights.Contracts;

namespace SkyRoute.Api.Flights.Services.Interfaces
{
    public interface IFlightSearchService
    {
        Task<List<SearchResponse>> SearchFlightsAsync(SearchRequest request);
    }
}
