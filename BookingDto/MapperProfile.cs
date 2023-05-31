using AutoMapper;
using BookingData.Models;
using BookingDto.Models.Driver;
using BookingDto.Models.Taxi;
using BookingDto.Models.User;

namespace BookingDto;
public class MapperProfile: Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Driver, DriverDto>().ReverseMap();
        CreateMap<Taxi, TaxiDto>().ReverseMap();
        CreateMap<Taxi, RequestBookingsForTaxiDto>().ReverseMap();
    }
}
