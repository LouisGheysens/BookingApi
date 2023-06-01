using BookingDto.Models.Payment;

namespace BookingDto.Models.Booking;
public record struct PaymentsForBookingDto(List<PaymentDto> paymentDtos);
