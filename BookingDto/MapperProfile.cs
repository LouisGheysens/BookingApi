using AutoMapper;
using BookingData.Models;
using BookingDto.Models;

namespace BookingDto;
public class MapperProfile: Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}
