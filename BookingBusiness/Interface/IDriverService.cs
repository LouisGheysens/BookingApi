using BookingDto.Models.Driver;

namespace BookingBusiness.Interface;
public interface IDriverService
{
    Task<List<DriverDto>> GetAllDrivers();
    Task<DriverDto> GetDriverById(int id);
    Task<DriverDto> CreateDriver(DriverDto dto);
    Task<DriverDto> UpdateDriver(int id, DriverDto dto);
    Task<bool> DeleteDriver(int id);
}
