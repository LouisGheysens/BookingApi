using AutoMapper;
using BookingBusiness.Interface;
using BookingData;
using BookingData.Models;
using BookingDto.Models.Booking;
using Microsoft.EntityFrameworkCore;
using BookingDataObject = BookingDto.Models.Booking.BookingDto;

namespace BookingBusiness.Services;
public class BookingService: IBookingService
{
    private readonly BookingDbContext _context;
    private readonly IMapper _mapper;

    public BookingService(BookingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private async Task<bool> HandleRequest(int id, BookingDataObject dto)
    {
        var booking = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == id && !x.Deleted);
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == dto.UserId && !x.Deleted);
        var taxi = await _context.Taxi.FirstOrDefaultAsync(x => x.Id == dto.TaxiId && !x.Deleted);
        return taxi != null && user != null && booking != null;
    }

    public async Task<BookingDataObject> CreateBooking(BookingDataObject dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == dto.UserId && !x.Deleted) ?? throw new Exception("User not found");
        var taxi = await _context.Taxi.FirstOrDefaultAsync(x => x.Id == dto.TaxiId && !x.Deleted) ?? throw new Exception("Taxi not found");

        var booking = _mapper.Map<Booking>(dto);
        booking.CreationDate = DateTime.Now;
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        return _mapper.Map<BookingDataObject>(dto);
    }

    public async Task<bool> DeleteBooking(int id)
    {
        var booking = _context.Bookings.FirstOrDefaultAsync(x => x.Id == id && !x.Deleted).Result ?? throw new Exception("Booking not found");
        booking.Deleted = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<BookingDataObject>> GetAllBookings()
    {
        var bookings = await _context.Bookings.Where(x => !x.Deleted).ToListAsync();
        return _mapper.Map<List<BookingDataObject>>(bookings);
    }

    public async Task<BookingDataObject> GetBookingById(int id)
    {
        var booking = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == id && !x.Deleted) ?? throw new Exception("Booking not found");

        return _mapper.Map<BookingDataObject>(booking);
    }

    public async Task<BookingDataObject> UpdateBooking(int id, BookingDataObject dto)
    {
        if(await HandleRequest(id, dto))
        {
            var booking = await _context.Bookings.FindAsync(id);
            booking.ModificationDate = DateTime.Now;
            _mapper.Map(dto, booking);
            await _context.SaveChangesAsync();
        }
        return dto;
    }

    public async Task<List<PaymentsForBookingDto>> GetAllBookingsIncludePayments()
    {
        var bookingsIncludePayments = await _context.Bookings.Include(x => x.Payments).ToListAsync();
        return _mapper.Map<List<PaymentsForBookingDto>>(bookingsIncludePayments);
    }

    public async Task<List<BookingDataObject>> GetAllBookingsFromPast()
    {
        var bookingsFromPast = await _context.Bookings.Where(x => x.Reservation < DateTime.Now).ToListAsync();
        return _mapper.Map<List<BookingDataObject>>(bookingsFromPast);
    }
    
    public async Task<List<BookingDataObject>> GetAllBookingsFromFuture()
    {
        var bookingsFromFuture = await _context.Bookings.Where(x => x.Reservation > DateTime.Now).ToListAsync();
        return _mapper.Map<List<BookingDataObject>>(bookingsFromFuture);
    }

    public async Task<PaymentsForBookingDto> AddPaymentsForBookingDto(int id, PaymentsForBookingDto paymentsForBookingDto)
    {
        var booking = await _context.Bookings
        .Include(b => b.Payments)
        .FirstOrDefaultAsync(x => x.Id == id && !x.Deleted) ?? throw new Exception("Booking not found");
        foreach (var paymentDto in paymentsForBookingDto.paymentDtos)
        {
            var payment = new Payment
            {
                Amount = paymentDto.Amount,
                PaymentMethod = paymentDto.PaymentMethod
            };

            booking.Payments.Add(payment);
        }

        await _context.SaveChangesAsync();

        var bookingDto = _mapper.Map<PaymentsForBookingDto>(booking);
        bookingDto.paymentDtos = paymentsForBookingDto.paymentDtos;

        return bookingDto;
    }
}
