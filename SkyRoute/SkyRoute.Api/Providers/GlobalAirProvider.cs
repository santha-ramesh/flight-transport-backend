using SkyRoute.Api.Flights.Contracts;
using SkyRoute.Api.Flights.Models;
using SkyRoute.Api.Providers.Interfaces;

namespace SkyRoute.Api.Providers
{
    public class GlobalAirProvider : IAirlineProvider
    {
        public string ProviderName => "GlobalAir";

        public Task<List<Flight>> SearchFlightsAsync(SearchRequest request)
        {
            var flights = new List<Flight>
            {
                new()
                {
                    Provider = ProviderName,
                    FlightNumber = "GA101",
                    Origin = request.Origin,
                    Destination = request.Destination,
                    DepartureTime = request.DepartureDate.AddHours(8),
                    ArrivalTime = request.DepartureDate.AddHours(15),
                    DurationMinutes = 420,
                    CabinClass = request.CabinClass,
                    BaseFare = 400
                },
                new()
                {
                    Provider = ProviderName,
                    FlightNumber = "GA102",
                    Origin = request.Origin,
                    Destination = request.Destination,
                    DepartureTime = request.DepartureDate.AddHours(10),
                    ArrivalTime = request.DepartureDate.AddHours(18),
                    DurationMinutes = 480,
                    CabinClass = request.CabinClass,
                    BaseFare = 450
                }
            };

            return Task.FromResult(flights);
        }
    }
}
