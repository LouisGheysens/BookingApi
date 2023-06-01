using AutoMapper;
using BookingBusiness.Interface;
using BookingData;
using BookingData.Models;
using BookingDto.Models.Taxi;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace BookingBusiness.Services;
public class TaxiService : ITaxiService
{
    private readonly BookingDbContext _context;
    private readonly IMapper _mapper;

    public TaxiService(BookingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TaxiDto> CreateTaxi(TaxiDto dto)
    {
        var driver = _context.Drivers.FirstOrDefaultAsync(x => x.Id == dto.driverId && !x.Deleted) ?? throw new Exception("Driver not found");

        var taxiRequest = _mapper.Map<Taxi>(dto);

        taxiRequest.CreationDate = DateTime.Now;    
        _context.Taxi.Add(taxiRequest);
        await _context.SaveChangesAsync();

        var dtoMapping = _mapper.Map<TaxiDto>(taxiRequest);

        return dtoMapping;
    }

    public async Task<bool> DeleteTaxi(int id)
    {
        var taxiRequest = _context.Taxi.FirstOrDefaultAsync(x => x.Id == id && !x.Deleted).Result ?? throw new Exception("Taxi not found");
        taxiRequest.Deleted = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<TaxiDto>> GetAllTaxis()
    {
        var taxiList = await _context.Taxi.Where(x => !x.Deleted).ToListAsync();
        return _mapper.Map<List<TaxiDto>>(taxiList);
    }

    public async Task<TaxiDto> GetTaxiById(int id)
    {
        var taxi = await _context.Taxi.FirstOrDefaultAsync(x => x.Id == id && !x.Deleted) ?? throw new Exception("Taxi not found"); ;
        return _mapper.Map<TaxiDto>(taxi);
    }

    public async Task<TaxiDto> UpdateTaxi(int id, TaxiDto dto)
    {
        var taxi = _context.Taxi.FirstOrDefault(x => x.Id == id && !x.Deleted) ?? throw new Exception("Taxi not found");
        var driver = _context.Drivers.FirstOrDefaultAsync(x => x.Id == dto.driverId && !x.Deleted) ?? throw new Exception("Driver not found");

        _mapper.Map(dto, taxi);

        taxi.ModificationDate = DateTime.Now;

        await _context.SaveChangesAsync();

        return dto;
    }

    public async Task<BookingsForTaxiDto> CreateBookingsForTaxi(int id, List<BookingDto.Models.Booking.BookingDto> bookings)
    {
        var taxi = _context.Taxi.FirstOrDefault(x => x.Id == id && !x.Deleted) ?? throw new Exception("Taxi not found");

        foreach(var booking in bookings)
        {
            var user = _context.Users.FirstOrDefaultAsync(x => x.Id == booking.UserId && !x.Deleted);
            if (user == null || string.IsNullOrEmpty(booking.UserId.ToString()))
            {
                throw new Exception("User not found");
            }

            if (taxi.Id != booking.TaxiId || string.IsNullOrEmpty(booking.TaxiId.ToString()))
            {
                throw new Exception("Taxi not found");
            }

            var newBooking = _mapper.Map<Booking>(booking);
            _context.Bookings.Add(newBooking);
        }

        await _context.SaveChangesAsync();
        var taxiDto = _mapper.Map<BookingsForTaxiDto>(taxi);
        taxiDto.bookings = bookings;
        return taxiDto;
    }

    public async Task<List<BookingsForTaxiDto>> GetAllTaxisIncludeBookings()
    {
        var taxiWithBookings = await _context.Taxi.Where(x => !x.Deleted).Include(x => x.Bookings).ToListAsync();

        return _mapper.Map<List<BookingsForTaxiDto>>(taxiWithBookings);
    }
}
