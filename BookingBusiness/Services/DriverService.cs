using AutoMapper;
using BookingBusiness.Interface;
using BookingData;
using BookingData.Models;
using BookingDto.Models.Driver;
using Microsoft.EntityFrameworkCore;

namespace BookingBusiness.Services;
public class DriverService : IDriverService
{
    private readonly BookingDbContext _context;
    private readonly IMapper _mapper;
    private const string DRIVER_NOT_FOUND = "Driver not found";

    public DriverService(BookingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DriverDto> CreateDriver(DriverDto dto)
    {
        var driver = _mapper.Map<Driver>(dto);

        driver.CreationDate = DateTime.Now;

        await _context.Drivers.AddAsync(driver);
        await _context.SaveChangesAsync();

        return _mapper.Map<DriverDto>(driver);
    }

    public async Task<bool> DeleteDriver(int id)
    {
        var driver = await _context.Drivers.FirstOrDefaultAsync(x => x.Id == id && !x.Deleted) ?? throw new Exception(DRIVER_NOT_FOUND);
        driver.Deleted = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<DriverDto>> GetAllDrivers()
    {
        var driver = await _context.Drivers.Where(x => !x.Deleted).ToListAsync();   
        var driverDtos = _mapper.Map<List<DriverDto>>(driver);
        return driverDtos;
    }

    public async Task<DriverDto> GetDriverById(int id)
    {
        var driver = await _context.Drivers.FirstOrDefaultAsync(x => x.Id == id && !x.Deleted) ?? throw new Exception(DRIVER_NOT_FOUND);
        var driverDto = _mapper.Map<DriverDto>(driver);
        return driverDto;
    }

    public async Task<DriverDto> UpdateDriver(int id, DriverDto dto)
    {
        var driver = await _context.Drivers.FirstOrDefaultAsync(x => x.Id == id && !x.Deleted) ?? throw new Exception(DRIVER_NOT_FOUND);

        _mapper.Map(dto, driver);

        driver.ModificationDate = DateTime.Now;

        await _context.SaveChangesAsync();

        return _mapper.Map<DriverDto>(driver);
    }
}
