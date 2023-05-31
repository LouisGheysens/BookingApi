namespace BookingDto.Models.Taxi;
public record struct RequestBookingsForTaxiDto(List<Booking.BookingDto> bookings)
{
}
