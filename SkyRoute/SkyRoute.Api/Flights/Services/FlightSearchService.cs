using SkyRoute.Api.Flights.Contracts;
using SkyRoute.Api.Flights.Services.Interfaces;
using SkyRoute.Api.Pricing.Interfaces;
using SkyRoute.Api.Providers.Interfaces;

namespace SkyRoute.Api.Flights.Services
{
    public class FlightSearchService : IFlightSearchService
    {
        private readonly IEnumerable<IAirlineProvider> _providers;
        private readonly IPricingService _pricingService;

        public FlightSearchService(IEnumerable<IAirlineProvider> providers, IPricingService pricingService)
        {
            _providers = providers;
            _pricingService = pricingService;
        }

        public async Task<List<SearchResponse>> SearchFlightsAsync(SearchRequest request)
        {
            var tasks = _providers.Select(p => p.SearchFlightsAsync(request));

            var results = await Task.WhenAll(tasks);

            var flights = results.SelectMany(x => x).ToList();

            var response = flights
                .Select(f =>
                {
                    var pricedFlight = _pricingService.ApplyPricing(f, request.Passengers);

                    return new SearchResponse
                    {
                        Provider = pricedFlight.Provider,
                        FlightNumber = pricedFlight.FlightNumber,
                        Origin = pricedFlight.Origin,
                        Destination = pricedFlight.Destination,
                        DepartureTime = pricedFlight.DepartureTime,
                        ArrivalTime = pricedFlight.ArrivalTime,
                        DurationMinutes = pricedFlight.DurationMinutes,
                        CabinClass = pricedFlight.CabinClass,
                        PricePerPassenger = pricedFlight.PricePerPassenger,
                        TotalPrice = pricedFlight.TotalPrice
                    };
                })
                .ToList();

            return response;
        }
    }
}
