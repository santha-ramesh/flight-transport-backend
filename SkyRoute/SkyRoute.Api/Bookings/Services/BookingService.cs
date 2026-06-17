using SkyRoute.Api.Bookings.Contracts;
using SkyRoute.Api.Bookings.Services.Interfaces;
using SkyRoute.Api.Shared.Exceptions;
using SkyRoute.Api.Shared.Helpers;

namespace SkyRoute.Api.Bookings.Services
{
    public class BookingService : IBookingService
    {
        public Task<BookingResponse> CreateBookingAsync(BookingRequest request)
        {
            var validDocument = DocumentValidationHelper.IsValidDocument(request.Origin, request.Destination, request.DocumentNumber);

            if (!validDocument)
            {
                throw new ValidationException("Invalid document number.");
            }

            var bookingReference = BookingReferenceGenerator.Generate();

            var response = new BookingResponse
            {
                BookingReference = bookingReference,
                Message = "Booking confirmed."
            };

            return Task.FromResult(response);
        }
    }
}
