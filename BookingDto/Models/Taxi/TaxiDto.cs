using BookingData.Enum;

namespace BookingDto.Models.Taxi;
public record struct TaxiDto(int Id, string brand, string model, string licensePlate, string color, DateTime yearOfCommissioning,
    int seatingCapacity, bool available, string milleage, FuelType fueltype, string location, int driverId)
{
}
