using SkyRoute.Api.Bookings.Contracts;

namespace SkyRoute.Api.Bookings.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BookingResponse> CreateBookingAsync(BookingRequest request);
    }
}
