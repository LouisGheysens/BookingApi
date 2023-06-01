using BookingBusiness.Interface;
using BookingDataObject = BookingDto.Models.Booking.BookingDto;
using Microsoft.AspNetCore.Mvc;
using BookingDto.Models.Booking;

namespace BookingApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBookings()
    {
        var request = await _bookingService.GetAllBookings();
        return Ok(request);
    }

    [HttpGet("BookingsFromPast")]
    public async Task<IActionResult> GetAllBookingsFromThePast()
    {
        var request = await _bookingService.GetAllBookingsFromPast();
        return Ok(request);
    }

    [HttpGet("BookingsFromFuture")]
    public async Task<IActionResult> GetAllBookingsFromTheFuture()
    {
        var request = await _bookingService.GetAllBookingsFromFuture();
        return Ok(request);
    }

    [HttpGet("IncludePayments")]
    public async Task<IActionResult> GetAllBookingsIncludePayments()
    {
        var request = await _bookingService.GetAllBookingsIncludePayments();
        return Ok(request);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetBookingById([FromRoute] int Id)
    {
        var request = await _bookingService.GetBookingById(Id);
        return Ok(request);
    }

    [HttpPost("Save")]
    public async Task<IActionResult> PostBooking([FromBody] BookingDataObject dto)
    {
        var request = await _bookingService.CreateBooking(dto);
        return CreatedAtAction(nameof(PostBooking), new { request });
    }

    [HttpPut("Update/{Id}")]
    public async Task<IActionResult> PutBooking([FromRoute] int Id, [FromBody] BookingDataObject dto)
    {
        var request = await _bookingService.UpdateBooking(Id, dto);
        return Ok(request);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteBooking([FromRoute] int Id)
    {
        var request = await _bookingService.DeleteBooking(Id);
        return Ok(request);
    }

    [HttpPut("AddPaymentsForBooking/{Id}")]
    public async Task<IActionResult> AddPaymentsForBooking([FromRoute] int Id, [FromBody] PaymentsForBookingDto dto)
    {
        var request = await _bookingService.AddPaymentsForBookingDto(Id, dto);
        return Ok(request);
    }
}
