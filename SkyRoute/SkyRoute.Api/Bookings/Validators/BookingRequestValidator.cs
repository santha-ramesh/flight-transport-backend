using FluentValidation;
using SkyRoute.Api.Bookings.Contracts;

namespace SkyRoute.Api.Bookings.Validators
{
    public class BookingRequestValidator : AbstractValidator<BookingRequest>
    {
        public BookingRequestValidator()
        {
            RuleFor(x => x.FlightNumber).NotEmpty();

            RuleFor(x => x.Provider).NotEmpty();

            RuleFor(x => x.FullName).NotEmpty().MaximumLength(100);

            RuleFor(x => x.Email).NotEmpty().EmailAddress();

            RuleFor(x => x.DocumentNumber).NotEmpty();

            RuleFor(x => x.PassengerCount).InclusiveBetween(1, 9);

            RuleFor(x => x.PricePerPassenger).GreaterThan(0);
        }
    }
}
