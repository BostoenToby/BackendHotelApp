namespace Hotels.Models;

public class RoomType
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string Id { get; set; }
    public string Name { get; set; }
    public int NumberOfBeds { get; set; }
}