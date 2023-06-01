namespace BookingDto.Models.Taxi;
public record struct BookingsForTaxiDto(List<Booking.BookingDto> bookings);