using BookingData.Base;

namespace BookingData.Models;
public class Booking: BaseModel
{
    public Booking()
    {
        Payments = new HashSet<Payment>();
    }

    public int Id { get; set; }

    public required string PickupLocation { get; set; }

    public required string DropOffLocation { get; set; }

    public int UserId { get; set; }
    public required User User { get; set; }

    public int TaxiId { get; set; }

    public required Taxi Taxi { get; set; }

    public DateTime Reservation { get; set; }

    public ICollection<Payment> Payments { get; set; }

}
