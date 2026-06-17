using Moq;
using SkyRoute.Api.Flights.Contracts;
using SkyRoute.Api.Flights.Models;
using SkyRoute.Api.Flights.Services;
using SkyRoute.Api.Pricing.Interfaces;
using SkyRoute.Api.Providers.Interfaces;

namespace SkyRoute.Api.Tests.Flights
{
    [TestClass]
    public class FlightSearchServiceTests
    {
        private Mock<IAirlineProvider> _globalAirProviderMock = null!;
        private Mock<IAirlineProvider> _budgetWingsProviderMock = null!;
        private Mock<IPricingService> _pricingServiceMock = null!;

        private FlightSearchService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            _globalAirProviderMock = new Mock<IAirlineProvider>();

            _budgetWingsProviderMock = new Mock<IAirlineProvider>();

            _pricingServiceMock = new Mock<IPricingService>();

            var providers = new List<IAirlineProvider>
            {
                _globalAirProviderMock.Object,
                _budgetWingsProviderMock.Object
            };

            _service = new FlightSearchService(providers, _pricingServiceMock.Object);
        }

        [TestMethod]
        public async Task SearchFlights_Should_ReturnFlights_FromAllProviders()
        {
            // Arrange

            var request = CreateSearchRequest();

            var globalFlights = new List<Flight>
            {
                CreateFlight("GlobalAir", "GA101", 100)
            };

            var budgetFlights = new List<Flight>
            {
                CreateFlight("BudgetWings","BW201",90)
            };

            _globalAirProviderMock
                .Setup(x => x.SearchFlightsAsync(request))
                .ReturnsAsync(globalFlights);

            _budgetWingsProviderMock
                .Setup(x => x.SearchFlightsAsync(request))
                .ReturnsAsync(budgetFlights);

            _pricingServiceMock
                .Setup(x => x.ApplyPricing(
                    It.IsAny<Flight>(),
                    request.Passengers))
                .Returns((Flight f, int _) => f);

            // Act

            var result = await _service.SearchFlightsAsync(request);

            // Assert

            Assert.AreEqual(2, result.Count);

            Assert.IsTrue(result.Any(x => x.Provider == "GlobalAir"));

            Assert.IsTrue(result.Any(x => x.Provider == "BudgetWings"));
        }

        [TestMethod]
        public async Task SearchFlights_Should_ApplyPricing_ToEveryFlight()
        {
            // Arrange

            var request = CreateSearchRequest();

            var flights = new List<Flight>
            {
                CreateFlight("GlobalAir","GA101",100),
                CreateFlight("GlobalAir","GA102",150)
            };

            _globalAirProviderMock
                .Setup(x => x.SearchFlightsAsync(request))
                .ReturnsAsync(flights);

            _budgetWingsProviderMock
                .Setup(x => x.SearchFlightsAsync(request))
                .ReturnsAsync(new List<Flight>());

            _pricingServiceMock
                .Setup(x => x.ApplyPricing(
                    It.IsAny<Flight>(),
                    request.Passengers))
                .Returns((Flight f, int _) => f);

            // Act

            await _service.SearchFlightsAsync(request);

            // Assert

            _pricingServiceMock.Verify(
                x => x.ApplyPricing(
                    It.IsAny<Flight>(),
                    request.Passengers),
                Times.Exactly(2));
        }

        [TestMethod]
        public async Task SearchFlights_Should_ReturnEmptyList_WhenNoFlightsFound()
        {
            // Arrange

            var request = CreateSearchRequest();

            _globalAirProviderMock
                .Setup(x => x.SearchFlightsAsync(request))
                .ReturnsAsync(new List<Flight>());

            _budgetWingsProviderMock
                .Setup(x => x.SearchFlightsAsync(request))
                .ReturnsAsync(new List<Flight>());

            // Act

            var result = await _service.SearchFlightsAsync(request);

            // Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task SearchFlights_Should_ReturnFlights_WhenOneProviderReturnsEmpty()
        {
            // Arrange

            var request = CreateSearchRequest();

            _globalAirProviderMock
                .Setup(x => x.SearchFlightsAsync(request))
                .ReturnsAsync(new List<Flight>
                {
                    CreateFlight("GlobalAir", "GA101", 100)
                });

            _budgetWingsProviderMock
                .Setup(x => x.SearchFlightsAsync(request))
                .ReturnsAsync(new List<Flight>());

            _pricingServiceMock
                .Setup(x => x.ApplyPricing(
                    It.IsAny<Flight>(),
                    request.Passengers))
                .Returns((Flight f, int _) => f);

            // Act

            var result =
                await _service.SearchFlightsAsync(request);

            // Assert

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual("GlobalAir", result[0].Provider);
        }

        [TestMethod]
        public async Task SearchFlights_Should_MapFlightPropertiesCorrectly()
        {
            // Arrange

            var request = CreateSearchRequest();

            var flight = CreateFlight("GlobalAir", "GA101", 115);

            flight.PricePerPassenger = 115;
            flight.TotalPrice = 230;

            _globalAirProviderMock
                .Setup(x => x.SearchFlightsAsync(request))
                .ReturnsAsync(new List<Flight> { flight });

            _budgetWingsProviderMock
                .Setup(x => x.SearchFlightsAsync(request))
                .ReturnsAsync(new List<Flight>());

            _pricingServiceMock
                .Setup(x => x.ApplyPricing(
                    It.IsAny<Flight>(),
                    request.Passengers))
                .Returns(flight);

            // Act

            var result = await _service.SearchFlightsAsync(request);

            // Assert

            var response = result.Single();

            Assert.AreEqual("GA101", response.FlightNumber);

            Assert.AreEqual("GlobalAir", response.Provider);

            Assert.AreEqual(115m, response.PricePerPassenger);

            Assert.AreEqual(230m, response.TotalPrice);
        }

        [TestMethod]
        public async Task SearchFlights_Should_ThrowException_WhenProviderFails()
        {
            // Arrange

            var request = CreateSearchRequest();

            _globalAirProviderMock
                .Setup(x => x.SearchFlightsAsync(request))
                .ThrowsAsync(
                    new Exception("Provider unavailable"));

            _budgetWingsProviderMock
                .Setup(x => x.SearchFlightsAsync(request))
                .ReturnsAsync(new List<Flight>());

            // Act + Assert

            await Assert.ThrowsExactlyAsync<Exception>(
                () => _service.SearchFlightsAsync(request));
        }

        private static SearchRequest CreateSearchRequest()
        {
            return new SearchRequest
            {
                Origin = "JFK",
                Destination = "LHR",
                DepartureDate =
                    DateTime.UtcNow.AddDays(10),
                Passengers = 2,
                CabinClass = "Economy"
            };
        }

        private static Flight CreateFlight(
            string provider,
            string flightNumber,
            decimal price)
        {
            return new Flight
            {
                Provider = provider,
                FlightNumber = flightNumber,
                Origin = "JFK",
                Destination = "LHR",
                DepartureTime =
                    DateTime.UtcNow.AddDays(1),
                ArrivalTime =
                    DateTime.UtcNow.AddDays(1).AddHours(7),
                DurationMinutes = 420,
                CabinClass = "Economy",
                BaseFare = price,
                PricePerPassenger = price,
                TotalPrice = price * 2
            };
        }
    }
}
