namespace Hotels.Models;

public class Hotel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string? Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string Province { get; set; }
    public string? Description { get; set; }
    public float? StarRating { get; set; }
    public string[]? Images { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public double PricePerNightMin { get; set; }
    public double PricePerNightMax { get; set; }
    public Review[]? Reviews { get; set; }
}