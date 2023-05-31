using BookingData.Base;
using BookingData.Enum;

namespace BookingData.Models;
public class Payment: BaseModel
{
    public Payment()
    {
        Bookings = new HashSet<Booking>();
    }

    public int Id { get; set; }

    public required string Amount { get; set; }  

    public PaymentMethod PaymentMethod { get; set; }

    public ICollection<Booking> Bookings { get; set; }
}
