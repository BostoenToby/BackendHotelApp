namespace Hotels.Models;

public class Review
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string Id { get; set; }
    public string Author { get; set; }
    public float StarRating { get; set; }
    public string? Image { get; set; }
    public string ReviewDescription { get; set; }


}