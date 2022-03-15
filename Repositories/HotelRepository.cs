namespace Hotels.Repositories;

public interface IHotelRepository
{
    Task<Hotel> AddHotel(Hotel newHotel);
    Task DeleteHotel(string id);
    Task<List<Hotel>> GetAllHotels();
    Task<Hotel> GetHotelById(string Id);
    Task<Hotel> GetHotelByName(string Name);
    Task<List<Hotel>> GetHotelsByProvinceAndStars(string Province, int Stars);
    Task<List<Hotel>> GetHotelsByCity(string City);
    Task<List<Hotel>> GetHotelsByCityAndStars(string City, int Stars);
    Task<List<Hotel>> GetHotelsByProvince(string Province);
    Task<List<Hotel>> GetHotelsByStars(int Stars);
    Task<Hotel> UpdateHotel(Hotel hotel);
}

public class HotelRepository : IHotelRepository
{
    private readonly IMongoContext _context;

    public HotelRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<List<Hotel>> GetAllHotels() => await _context.HotelsCollection.Find<Hotel>(_ => true).ToListAsync();
    public async Task<Hotel> GetHotelById(string Id) => await _context.HotelsCollection.Find<Hotel>(c => c.Id == Id).FirstOrDefaultAsync();
    public async Task<Hotel> GetHotelByName(string Name) => await _context.HotelsCollection.Find<Hotel>(c => c.Name == Name).FirstOrDefaultAsync();
    public async Task<List<Hotel>> GetHotelsByCity(string City) => await _context.HotelsCollection.Find<Hotel>(c => c.City == City).ToListAsync();
    public async Task<List<Hotel>> GetHotelsByProvince(string Province) => await _context.HotelsCollection.Find<Hotel>(c => c.Province == Province).ToListAsync();
    public async Task<List<Hotel>> GetHotelsByStars(int Stars) => await _context.HotelsCollection.Find<Hotel>(c => c.StarRating >= Stars).ToListAsync();
    public async Task<List<Hotel>> GetHotelsByProvinceAndStars(string Province, int Stars) => await _context.HotelsCollection.Find<Hotel>(c => (c.StarRating >= Stars) && (c.Province == Province)).ToListAsync();
    public async Task<List<Hotel>> GetHotelsByCityAndStars(string City, int Stars) => await _context.HotelsCollection.Find<Hotel>(c => (c.StarRating >= Stars) && (c.City == City)).ToListAsync();
    public async Task<Hotel> AddHotel(Hotel newHotel)
    {
        await _context.HotelsCollection.InsertOneAsync(newHotel);
        return newHotel;
    }
    public async Task<Hotel> UpdateHotel(Hotel hotel)
    {
        try
        {
            var filter = Builders<Hotel>.Filter.Eq("Id", hotel.Id);
            var update = Builders<Hotel>.Update.Set("Name", hotel.Name).Set("City", hotel.City).Set("Address", hotel.Address).Set("Province", hotel.Province).Set("Description", hotel.Description).Set("StarRating", hotel.StarRating).Set("Images", hotel.Images).Set("Longitude", hotel.Longitude).Set("Latitude", hotel.Latitude).Set("PricePerNight", hotel.PricePerNight).Set("Reviews", hotel.Reviews);
            var result = await _context.HotelsCollection.UpdateOneAsync(filter, update);
            return await GetHotelById(hotel.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
    public async Task DeleteHotel(string id)
    {
        try
        {
            var filter = Builders<Hotel>.Filter.Eq("Id", id);
            var result = await _context.HotelsCollection.DeleteOneAsync(filter);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}