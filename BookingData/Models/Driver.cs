using BookingData.Base;

namespace BookingData.Models;
public class Driver: BaseModel
{
    public int Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public required string LicenseNumber { get; set; }

    public int TaxiId { get; set; }
}
