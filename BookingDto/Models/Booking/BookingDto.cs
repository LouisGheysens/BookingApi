namespace BookingDto.Models.Booking;
public record struct BookingDto(int TaxiId, int UserId, DateTime Reservation, string PickupLocation, string DropOffLocation);
