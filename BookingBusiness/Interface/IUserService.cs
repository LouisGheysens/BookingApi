using BookingData.Models;
using BookingDto.Models;

namespace BookingBusiness.Interface;
public interface IUserService
{
    User Register(UserDto request);
    string Login(UserDto request);
    string CreateToken(User request);
}
