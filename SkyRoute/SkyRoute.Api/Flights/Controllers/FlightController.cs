using Microsoft.AspNetCore.Mvc;
using SkyRoute.Api.Flights.Contracts;
using SkyRoute.Api.Flights.Services.Interfaces;

namespace SkyRoute.Api.Flights.Controllers
{
    [Route("api/flights")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightSearchService _service;

        public FlightController(IFlightSearchService service)
        {
            _service = service;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] SearchRequest request)
        {
            var result = await _service.SearchFlightsAsync(request);
            return Ok(result);
        }
    }
}
