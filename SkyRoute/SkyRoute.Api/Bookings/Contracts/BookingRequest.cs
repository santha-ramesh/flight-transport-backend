using System.ComponentModel.DataAnnotations;

namespace SkyRoute.Api.Bookings.Contracts
{
    public class BookingRequest
    {
        [Required]
        public string FlightNumber { get; set; } = string.Empty;

        [Required]
        public string Provider { get; set; } = string.Empty;

        [Required]
        public string Origin { get; set; } = string.Empty;

        [Required]
        public string Destination { get; set; } = string.Empty;

        [Range(1, 9, ErrorMessage = "Passenger count must be between 1 and 9.")]
        public int PassengerCount { get; set; }

        public decimal PricePerPassenger { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters.")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string DocumentNumber { get; set; } = string.Empty;
    }
}
