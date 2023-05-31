using AutoMapper;
using BookingBusiness.Interface;
using BookingData;
using BookingData.Models;
using BookingDto.Models.Taxi;
using Microsoft.EntityFrameworkCore;

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
        var taxi = _context.Taxi.FirstOrDefault(x => x.Id == id && !x.Deleted) ?? throw new Exception("Taxi not found"); ;

        _mapper.Map(taxi, dto);

        taxi.ModificationDate = DateTime.Now;

        await _context.SaveChangesAsync();

        return dto;
    }

    public async Task<RequestBookingsForTaxiDto> CreateBookingsForTaxi(int id, List<BookingDto.Models.Booking.BookingDto> bookings)
    {
        var taxi = _context.Taxi.FirstOrDefault(x => x.Id == id && !x.Deleted) ?? throw new Exception("Taxi not found");

        var taxiDto = _mapper.Map<RequestBookingsForTaxiDto>(taxi);

        foreach(var booking in bookings)
        {
            taxiDto.bookings.Add(booking);
        }

        await _context.SaveChangesAsync();

        return taxiDto;
    }

}
