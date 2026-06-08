using SkyRoute.Api.Flights.Contracts;
using SkyRoute.Api.Flights.Models;
using SkyRoute.Api.Providers.Interfaces;

namespace SkyRoute.Api.Providers
{
    public class BudgetWingsProvider : IAirlineProvider
    {
        public string ProviderName => "BudgetWings";

        public Task<List<Flight>> SearchFlightsAsync(SearchRequest request)
        {
            var flights = new List<Flight>
            {
                new()
                {
                    Provider = ProviderName,
                    FlightNumber = "BW201",
                    Origin = request.Origin,
                    Destination = request.Destination,
                    DepartureTime = request.DepartureDate.AddHours(6),
                    ArrivalTime = request.DepartureDate.AddHours(14),
                    DurationMinutes = 480,
                    CabinClass = request.CabinClass,
                    BaseFare = 300
                },
                new()
                {
                    Provider = ProviderName,
                    FlightNumber = "BW202",
                    Origin = request.Origin,
                    Destination = request.Destination,
                    DepartureTime = request.DepartureDate.AddHours(12),
                    ArrivalTime = request.DepartureDate.AddHours(19),
                    DurationMinutes = 420,
                    CabinClass = request.CabinClass,
                    BaseFare = 350
                }
            };

            return Task.FromResult(flights);
        }
    }
}
