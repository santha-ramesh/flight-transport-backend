namespace SkyRoute.Api.Bookings.Models
{
    public class Booking
    {
        public string BookingReference { get; set; } = string.Empty;

        public string FlightNumber { get; set; } = string.Empty;

        public string Provider { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string DocumentNumber { get; set; } = string.Empty;

        public decimal TotalPrice { get; set; }
    }
}
