using BookingData.Enum;

namespace BookingDto.Models.Payment;
public record struct PaymentDto(string Amount, PaymentMethod PaymentMethod);
