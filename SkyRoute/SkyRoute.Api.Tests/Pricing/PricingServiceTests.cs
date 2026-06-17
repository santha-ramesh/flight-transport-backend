using SkyRoute.Api.Flights.Models;
using SkyRoute.Api.Pricing;

namespace SkyRoute.Api.Tests.Pricing
{
    [TestClass]
    public class PricingServiceTests
    {
        private PricingService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            _service = new PricingService();
        }

        [TestMethod]
        public void ApplyPricing_Should_Add15PercentSurcharge_ForGlobalAir()
        {
            // Arrange
            var flight = new Flight
            {
                Provider = "GlobalAir",
                BaseFare = 100m
            };

            // Act
            var result = _service.ApplyPricing(flight, 1);

            // Assert
            Assert.AreEqual(115.00m, result.PricePerPassenger);
        }

        [TestMethod]
        public void ApplyPricing_Should_RoundGlobalAirPrice_To2DecimalPlaces()
        {
            // Arrange
            var flight = new Flight
            {
                Provider = "GlobalAir",
                BaseFare = 123.456m
            };

            // Act
            var result = _service.ApplyPricing(flight, 1);

            // Assert
            Assert.AreEqual(141.97m, result.PricePerPassenger);
        }

        [TestMethod]
        public void ApplyPricing_Should_CalculateTotalPrice_ForGlobalAir()
        {
            // Arrange
            var flight = new Flight
            {
                Provider = "GlobalAir",
                BaseFare = 100m
            };

            // Act
            var result = _service.ApplyPricing(flight, 2);

            // Assert
            Assert.AreEqual(115.00m, result.PricePerPassenger);
            Assert.AreEqual(230.00m, result.TotalPrice);
        }

        [TestMethod]
        public void ApplyPricing_Should_Apply10PercentDiscount_ForBudgetWings()
        {
            // Arrange
            var flight = new Flight
            {
                Provider = "BudgetWings",
                BaseFare = 100m
            };

            // Act
            var result = _service.ApplyPricing(flight, 1);

            // Assert
            Assert.AreEqual(90.00m, result.PricePerPassenger);
        }

        [TestMethod]
        public void ApplyPricing_Should_CalculateTotalPrice_ForBudgetWings()
        {
            // Arrange
            var flight = new Flight
            {
                Provider = "BudgetWings",
                BaseFare = 100m
            };

            // Act
            var result = _service.ApplyPricing(flight, 3);

            // Assert
            Assert.AreEqual(90.00m, result.PricePerPassenger);
            Assert.AreEqual(270.00m, result.TotalPrice);
        }

        [TestMethod]
        public void ApplyPricing_Should_ApplyMinimumPrice_WhenBudgetWingsDiscountFallsBelowThreshold()
        {
            // Arrange
            var flight = new Flight
            {
                Provider = "BudgetWings",
                BaseFare = 20m
            };

            // Act
            var result = _service.ApplyPricing(flight, 1);

            // Assert
            Assert.AreEqual(29.99m, result.PricePerPassenger);
        }

        [TestMethod]
        public void ApplyPricing_Should_UseMinimumPrice_ForMultiplePassengers()
        {
            // Arrange
            var flight = new Flight
            {
                Provider = "BudgetWings",
                BaseFare = 20m
            };

            // Act
            var result = _service.ApplyPricing(flight, 2);

            // Assert
            Assert.AreEqual(29.99m, result.PricePerPassenger);
            Assert.AreEqual(59.98m, result.TotalPrice);
        }

        [TestMethod]
        public void ApplyPricing_Should_NotApplyMinimumPrice_WhenDiscountedPriceExceedsThreshold()
        {
            // Arrange
            var flight = new Flight
            {
                Provider = "BudgetWings",
                BaseFare = 50m
            };

            // Act
            var result = _service.ApplyPricing(flight, 1);

            // Assert
            Assert.AreEqual(45.00m, result.PricePerPassenger);
        }
    }
}
