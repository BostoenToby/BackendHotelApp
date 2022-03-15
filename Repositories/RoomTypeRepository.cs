namespace Hotels.Repositories;

public interface IRoomTypeRepository
{
    Task AddRoomType(RoomType newRoomType);
    Task DeleteRoomType(string Id);
    Task<List<RoomType>> GetAllRoomTypes();
    Task<RoomType> GetRoomTypeById(string Id);
    Task<RoomType> GetRoomTypeByName(string Name);
    Task<List<RoomType>> GetRoomTypesByNumberOfBeds(int NumberOfBeds);
    Task<RoomType> UpdateRoomType(RoomType roomType);
}

public class RoomTypeRepository : IRoomTypeRepository
{
    private readonly IMongoContext _context;

    public RoomTypeRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<List<RoomType>> GetAllRoomTypes() => await _context.RoomTypeCollection.Find(_ => true).ToListAsync();
    public async Task<RoomType> GetRoomTypeById(string Id) => await _context.RoomTypeCollection.Find(c => c.Id == Id).FirstOrDefaultAsync();
    public async Task<RoomType> GetRoomTypeByName(string Name) => await _context.RoomTypeCollection.Find(c => c.Name == Name).FirstOrDefaultAsync();
    public async Task<List<RoomType>> GetRoomTypesByNumberOfBeds(int NumberOfBeds) => await _context.RoomTypeCollection.Find(c => c.NumberOfBeds == NumberOfBeds).ToListAsync();
    public async Task AddRoomType(RoomType newRoomType) => await _context.RoomTypeCollection.InsertOneAsync(newRoomType);
    public async Task<RoomType> UpdateRoomType(RoomType roomType)
    {
        try
        {
            var filter = Builders<RoomType>.Filter.Eq("Id", roomType.Id);
            var update = Builders<RoomType>.Update.Set("Name", roomType.Name).Set("NumberOfBeds", roomType.NumberOfBeds);
            var result = await _context.RoomTypeCollection.UpdateOneAsync(filter, update);
            return await GetRoomTypeById(roomType.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
    public async Task DeleteRoomType(string Id)
    {
        try
        {
            var filter = Builders<RoomType>.Filter.Eq("Id", Id);
            var result = await _context.RoomTypeCollection.DeleteOneAsync(filter);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}