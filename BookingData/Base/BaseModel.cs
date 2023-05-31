namespace BookingData.Base;
public abstract class BaseModel
{
    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public bool Deleted { get; set; }
}
