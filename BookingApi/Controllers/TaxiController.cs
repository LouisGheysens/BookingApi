using BookingBusiness.Interface;
using BookingDto.Models.Taxi;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TaxiController : ControllerBase
{
    private readonly ITaxiService _taxiService;

    public TaxiController(ITaxiService taxiService)
    {
        _taxiService = taxiService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTaxis()
    {
        var request = await _taxiService.GetAllTaxis();
        return Ok(request);
    }


    [HttpGet("{Id}")]
    public async Task<IActionResult> GetTaxiById(int Id)
    {
        var request = await _taxiService.GetTaxiById(Id);
        return Ok(request);
    }

    [HttpPost("Save")]
    public async Task<IActionResult> PostTaxi([FromBody] TaxiDto dto)
    {
        var request = await _taxiService.CreateTaxi(dto);
        return CreatedAtAction(nameof(PostTaxi), new { request });
    }

    [HttpPut("Update/{Id}")]
    public async Task<IActionResult> PutTaxi([FromRoute] int Id, [FromBody] TaxiDto dto)
    {
        var request = await _taxiService.UpdateTaxi(Id, dto);
        return Ok(request);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteTaxi(int Id)
    {
        var request = await _taxiService.DeleteTaxi(Id);
        return Ok(request);
    }

    [HttpPut("Bookings/{Id}")]
    public async Task<IActionResult> PutBookinsForTaxi([FromRoute] int Id, [FromBody] RequestBookingsForTaxiDto dto)
    {
        var request = await _taxiService.CreateBookingsForTaxi(Id, dto.bookings);
        return Ok(request);
    }


}
