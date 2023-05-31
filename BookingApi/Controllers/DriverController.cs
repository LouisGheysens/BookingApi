using BookingBusiness.Interface;
using BookingDto.Models.Driver;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DriverController : ControllerBase
{
    private readonly IDriverService _driverService;

    public DriverController(IDriverService driverService)
    {
        _driverService = driverService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDrivers()
    {
        var drivers = await _driverService.GetAllDrivers();
        return Ok(drivers);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetDriverById([FromRoute] int Id)
    {
        var driver = await _driverService.GetDriverById(Id);
        return Ok(driver);
    }

    [HttpPost("Save")]
    public async Task <IActionResult> PostDriver([FromBody] DriverDto driverRequest)
    {
        var request = await _driverService.CreateDriver(driverRequest);
        return CreatedAtAction(nameof(PostDriver), new { request } );
    }

    [HttpPut("Update/{Id}")]
    public async Task<IActionResult> PutDriver([FromRoute] int Id, [FromBody] DriverDto driverRequest)
    {
        var request = await _driverService.UpdateDriver(Id, driverRequest);
        return Ok(request);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteDriver([FromRoute] int Id)
    {
        var request = await _driverService.DeleteDriver(Id);
        return Ok(request);
    }
}
