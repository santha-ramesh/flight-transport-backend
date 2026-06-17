using Microsoft.AspNetCore.Mvc;
using SkyRoute.Api.Bookings.Contracts;
using SkyRoute.Api.Bookings.Services.Interfaces;

namespace SkyRoute.Api.Bookings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequest request)
        {
            var result = await _bookingService.CreateBookingAsync(request);
            return Ok(result);
        }
    }
}
