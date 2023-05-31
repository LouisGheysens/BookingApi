using BookingDto.Models.Taxi;

namespace BookingBusiness.Interface;
public interface ITaxiService
{
    Task<List<TaxiDto>> GetAllTaxis();
    Task<TaxiDto> GetTaxiById(int id);
    Task<TaxiDto> CreateTaxi(TaxiDto dto);
    Task<TaxiDto> UpdateTaxi(int id, TaxiDto dto);
    Task<bool> DeleteTaxi(int id);
    Task<RequestBookingsForTaxiDto> CreateBookingsForTaxi(int id, List<BookingDto.Models.Booking.BookingDto> bookings);
}
