namespace SkyRoute.Api.Flights.Contracts
{
    public class SearchRequest
    {
        public string Origin { get; set; } = string.Empty;

        public string Destination { get; set; } = string.Empty;

        public DateTime DepartureDate { get; set; }

        public int Passengers { get; set; }

        public string CabinClass { get; set; } = string.Empty;
    }
}
