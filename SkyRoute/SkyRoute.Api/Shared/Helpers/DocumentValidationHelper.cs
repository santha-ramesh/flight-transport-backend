using SkyRoute.Api.Shared.Models;
using System.Text.RegularExpressions;

namespace SkyRoute.Api.Shared.Helpers
{
    public static class DocumentValidationHelper
    {
        public static bool IsInternational(string origin, string destination)
        {
            var originAirport = AirportCatalog.Airports.FirstOrDefault(a => a.Code == origin);

            var destinationAirport = AirportCatalog.Airports.FirstOrDefault(a => a.Code == destination);

            if (originAirport == null || destinationAirport == null)
            {
                return false;
            }

            return originAirport.Country != destinationAirport.Country;
        }

        public static bool IsValidDocument(string origin, string destination, string documentNumber)
        {
            var isInternational = IsInternational(origin, destination);

            var pattern = isInternational ? @"^[A-Z0-9]{6,12}$" : @"^[A-Za-z0-9]{6,20}$";

            return Regex.IsMatch(documentNumber, pattern);
        }
    }
}
