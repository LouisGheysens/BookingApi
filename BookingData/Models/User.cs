using BookingData.Base;

namespace BookingData.Models;
public class User: BaseModel
{
    public User()
    {
        Bookings = new HashSet<Booking>();
    }

    public int Id { get; set; }

    public required string UserName { get; set; }

    public required string HashedPassword { get; set; }

    public DateTime LastLogin { get; set; }

    public ICollection<Booking> Bookings { get; set; }   
}
