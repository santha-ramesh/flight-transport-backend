namespace SkyRoute.Api.Shared.Models
{
    public class AirportCatalog
    {
        public static readonly List<Airport> Airports =
        [
            new()
            {
                Code = "NY",
                Name = "New York",
                Country = "USA"
            },

            new()
            {
                Code = "LA",
                Name = "Los Angeles",
                Country = "USA"
            },

            new()
            {
                Code = "CHI",
                Name = "Chicago",
                Country = "USA"
            },

            new()
            {
                Code = "LON",
                Name = "London",
                Country = "UK"
            },

            new()
            {
                Code = "MAN",
                Name = "Manchester",
                Country = "UK"
            },

            new()
            {
                Code = "EDI",
                Name = "Edinburgh",
                Country = "UK"
            }
        ];
    }
}
