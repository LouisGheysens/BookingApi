using BookingBusiness.Interface;
using BookingData.Models;
using BookingDto.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public ActionResult<User> Register(UserDto request)
    {
        var registerRequest = _userService.Register(request);
        return Ok(registerRequest);
    }

    [HttpPost("login")]
    public ActionResult<User> Login(UserDto request)
    {
        var loginRequest = _userService.Login(request);
        return Ok(loginRequest);
    }
}
