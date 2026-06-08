using SkyRoute.Api.Flights.Models;
using SkyRoute.Api.Pricing.Interfaces;

namespace SkyRoute.Api.Pricing
{
    public class PricingService : IPricingService
    {
        public Flight ApplyPricing(Flight flight, int passengers)
        {
            decimal perPassengerPrice;

            switch (flight.Provider)
            {
                case "GlobalAir":
                    perPassengerPrice = Math.Round(flight.BaseFare * 1.15m, 2);
                    break;

                case "BudgetWings":
                    perPassengerPrice = Math.Max(flight.BaseFare * 0.90m, 29.99m);
                    break;

                default:
                    perPassengerPrice = flight.BaseFare;
                    break;
            }

            flight.PricePerPassenger = perPassengerPrice;
            flight.TotalPrice = perPassengerPrice * passengers;

            return flight;
        }
    }
}
