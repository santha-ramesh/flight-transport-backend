using SkyRoute.Api.Bookings.Contracts;
using SkyRoute.Api.Bookings.Services;
using SkyRoute.Api.Shared.Exceptions;

namespace SkyRoute.Api.Tests.Bookings;

[TestClass]
public class BookingServiceTests
{
    private BookingService _service = null!;

    [TestInitialize]
    public void Setup()
    {
        _service = new BookingService();
    }

    [TestMethod]
    public async Task CreateBookingAsync_Should_ReturnBookingReference_ForDomesticFlight()
    {
        // Arrange

        var request = new BookingRequest
        {
            FlightNumber = "GA101",
            Provider = "GlobalAir",
            Origin = "NY",
            Destination = "MAN",
            PassengerCount = 2,
            PricePerPassenger = 460,
            FullName = "John Smith",
            Email = "john@test.com",
            DocumentNumber = "ABC12345"
        };

        // Act

        var result = await _service.CreateBookingAsync(request);

        // Assert

        Assert.IsNotNull(result);

        Assert.IsFalse(string.IsNullOrWhiteSpace(result.BookingReference));

        Assert.IsTrue(result.BookingReference.StartsWith("SR-"));

        Assert.AreEqual("Booking confirmed.",result.Message);
    }

    [TestMethod]
    public async Task CreateBookingAsync_Should_ReturnBookingReference_ForInternationalFlight()
    {
        // Arrange

        var request = new BookingRequest
        {
            FlightNumber = "GA101",
            Provider = "GlobalAir",
            Origin = "NY",
            Destination = "CHI",
            PassengerCount = 1,
            PricePerPassenger = 500,
            FullName = "John Smith",
            Email = "john@test.com",
            DocumentNumber = "P1234567"
        };

        // Act

        var result = await _service.CreateBookingAsync(request);

        // Assert

        Assert.IsNotNull(result);

        Assert.IsTrue(result.BookingReference.StartsWith("SR-"));
    }

    [TestMethod]
    public async Task CreateBookingAsync_Should_GenerateReference_WithCorrectFormat()
    {
        // Arrange

        var request = CreateDomesticBooking();

        // Act

        var result = await _service.CreateBookingAsync(request);

        // Assert
        Assert.IsTrue(result.BookingReference.StartsWith("SR-"));

        Assert.AreEqual(9,result.BookingReference.Length);
    }

    [TestMethod]
    public async Task CreateBookingAsync_Should_GenerateUniqueReferences()
    {
        // Arrange

        var request = CreateDomesticBooking();

        // Act

        var booking1 = await _service.CreateBookingAsync(request);

        var booking2 = await _service.CreateBookingAsync(request);

        // Assert

        Assert.AreNotEqual(booking1.BookingReference, booking2.BookingReference);
    }

    [TestMethod]
    public async Task CreateBookingAsync_Should_ThrowValidationException_ForInvalidDomesticDocument()
    {
        // Arrange

        var request = new BookingRequest
        {
            FlightNumber = "GA101",
            Provider = "GlobalAir",
            Origin = "NY",
            Destination = "CHI",
            PassengerCount = 1,
            PricePerPassenger = 100,
            FullName = "John Smith",
            Email = "john@test.com",
            DocumentNumber = "@@@"
        };

        // Act + Assert

        await Assert.ThrowsAsync<ValidationException>(
            () => _service.CreateBookingAsync(request));
    }

    [TestMethod]
    public async Task CreateBookingAsync_Should_ThrowValidationException_ForInvalidPassport()
    {
        // Arrange

        var request = new BookingRequest
        {
            FlightNumber = "GA101",
            Provider = "GlobalAir",
            Origin = "NY",
            Destination = "MAN",
            PassengerCount = 1,
            PricePerPassenger = 100,
            FullName = "John Smith",
            Email = "john@test.com",
            DocumentNumber = "passport123"
        };

        // Act + Assert

        await Assert.ThrowsAsync<ValidationException>(
            () => _service.CreateBookingAsync(request));
    }

    [TestMethod]
    public async Task CreateBookingAsync_Should_AcceptMinimumValidPassportLength()
    {
        // Arrange

        var request = new BookingRequest
        {
            FlightNumber = "GA101",
            Provider = "GlobalAir",
            Origin = "NY",
            Destination = "CHI",
            PassengerCount = 1,
            PricePerPassenger = 100,
            FullName = "John Smith",
            Email = "john@test.com",
            DocumentNumber = "AB1234"
        };

        // Act

        var result = await _service.CreateBookingAsync(request);

        // Assert

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task CreateBookingAsync_Should_AcceptMaximumValidPassportLength()
    {
        // Arrange

        var request = new BookingRequest
        {
            FlightNumber = "GA101",
            Provider = "GlobalAir",
            Origin = "NY",
            Destination = "CHI",
            PassengerCount = 1,
            PricePerPassenger = 100,
            FullName = "John Smith",
            Email = "john@test.com",
            DocumentNumber = "AB1234567890"
        };

        // Act

        var result = await _service.CreateBookingAsync(request);

        // Assert

        Assert.IsNotNull(result);
    }

    //[TestMethod]
    //public async Task CreateBookingAsync_Should_RejectPassportLongerThan12Characters()
    //{
    //    // Arrange

    //    var request = new BookingRequest
    //    {
    //        FlightNumber = "GA101",
    //        Provider = "GlobalAir",
    //        Origin = "JFK",
    //        Destination = "LHR",
    //        PassengerCount = 1,
    //        PricePerPassenger = 100,
    //        FullName = "John Smith",
    //        Email = "john@test.com",
    //        DocumentNumber = "AB1234567890123"
    //    };

    //    // Act + Assert

    //    await Assert.ThrowsAsync<ValidationException>(
    //        () => _service.CreateBookingAsync(request));
    //}

    [TestMethod]
    public async Task CreateBookingAsync_Should_RejectDomesticIdShorterThan6Characters()
    {
        // Arrange

        var request = new BookingRequest
        {
            FlightNumber = "GA101",
            Provider = "GlobalAir",
            Origin = "JFK",
            Destination = "LAX",
            PassengerCount = 1,
            PricePerPassenger = 100,
            FullName = "John Smith",
            Email = "john@test.com",
            DocumentNumber = "123"
        };

        // Act + Assert

        await Assert.ThrowsAsync<ValidationException>(
            () => _service.CreateBookingAsync(request));
    }

    private static BookingRequest CreateDomesticBooking()
    {
        return new BookingRequest
        {
            FlightNumber = "GA101",
            Provider = "GlobalAir",
            Origin = "NY",
            Destination = "CHI",
            PassengerCount = 2,
            PricePerPassenger = 460,
            FullName = "John Smith",
            Email = "john@test.com",
            DocumentNumber = "ABC12345"
        };
    }
}