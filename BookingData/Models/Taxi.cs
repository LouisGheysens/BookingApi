using BookingData.Base;
using BookingData.Enum;

namespace BookingData.Models;
public class Taxi: BaseModel
{
    public Taxi()
    {
        Bookings = new HashSet<Booking>();
    }

    public int Id { get; set; }

    public required string Brand { get; set; }

    public required string Model { get; set; }

    public required string LicensePlate { get; set; }

    public required string Color { get; set; }

    public DateTime YearOfCommissioning { get; set; }

    public int SeatingCapacity { get; set; }

    public bool Available { get; set; }

    public required string Milleage { get; set; }

    public FuelType FuelType { get; set; }

    public required string Location { get; set; }

    public int DriverId { get; set; }

    public required Driver Driver { get; set; }

    public ICollection<Booking> Bookings { get; set; }




}
