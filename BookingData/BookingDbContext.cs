using BookingData.Models;
using BookingHelper;
using Microsoft.EntityFrameworkCore;

namespace BookingData;
public class BookingDbContext: DbContext
{
    public BookingDbContext(DbContextOptions<BookingDbContext> options) : base (options)
    {
        
    }

    public DbSet<Booking> Bookings { get; set; }

    public DbSet<Driver> Drivers { get; set; }

    public DbSet<Payment> Payments { get; set; }

    public DbSet<Taxi> Taxi { get; set; }

    public DbSet<User> Users { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        AppRetriever.GetConnectionString();
    }
}
