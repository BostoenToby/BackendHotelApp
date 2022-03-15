namespace Hotels.Configuration;

public class DatabaseSettings
{
    public string? ConnectionString { get; set; }
    public string? DatabaseName { get; set; }
    public string? HotelsCollection { get; set; }
    public string? ReviewsCollection { get; set; }
    public string? ReservationCollection { get; set; }
    public string? RoomTypeCollection { get; set; }
}