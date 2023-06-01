using BookingDto.Models.Booking;
using BookingDataObject = BookingDto.Models.Booking.BookingDto;
namespace BookingBusiness.Interface;
public interface IBookingService
{
    Task<List<BookingDataObject>> GetAllBookings();
    Task<List<BookingDataObject>> GetAllBookingsFromPast();
    Task<List<BookingDataObject>> GetAllBookingsFromFuture();
    Task<List<PaymentsForBookingDto>> GetAllBookingsIncludePayments();
    Task<BookingDataObject> GetBookingById(int id);
    Task<BookingDataObject> CreateBooking(BookingDataObject dto);
    Task<BookingDataObject> UpdateBooking(int id, BookingDataObject dto);
    Task<bool> DeleteBooking(int id);
    Task<PaymentsForBookingDto> AddPaymentsForBookingDto(int id, PaymentsForBookingDto paymentsForBookingDto);
}
