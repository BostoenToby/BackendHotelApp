namespace Hotels.Repositories;

public interface IReservationRepository
{
    Task AddReservation(Reservation NewReservation);
    Task DeleteReservation(string Id);
    Task<List<Reservation>> GetAllReservations();
    Task<Reservation> GetReservationById(string ReservationId);
    Task<List<Reservation>> GetReservationsByDate(DateTime Date);
    Task<List<Reservation>> GetReservationsByDateAndHotel(DateTime Date, string HotelName);
    Task<List<Reservation>> GetReservationsByHotel(string HotelName);
    Task<List<Reservation>> GetReservationsByName(string Name);
    Task<List<Reservation>> GetReservationsByNameAndDate(string Name, DateTime Date);
    Task<List<Reservation>> GetReservationsByNameAndHotel(string Name, string HotelName);
    Task<List<Reservation>> GetReservationsByNameAndHotelAndDate(string Name, string HotelName, DateTime Date);
    Task<Reservation> UpdateReservation(Reservation reservation);
}

public class ReservationRepository : IReservationRepository
{
    private readonly IMongoContext _context;

    public ReservationRepository(IMongoContext context)
    {
        _context = context;
    }

    // GET
    public async Task<List<Reservation>> GetAllReservations() => await _context.ReservationCollection.Find<Reservation>(_ => true).ToListAsync();
    public async Task<Reservation> GetReservationById(string ReservationId) => await _context.ReservationCollection.Find<Reservation>(c => c.Id == ReservationId).FirstOrDefaultAsync();
    public async Task<List<Reservation>> GetReservationsByHotel(string HotelName) => await _context.ReservationCollection.Find<Reservation>(c => c.Hotel.Name == HotelName).ToListAsync();
    public async Task<List<Reservation>> GetReservationsByName(string Name) => await _context.ReservationCollection.Find<Reservation>(c => c.Name == Name).ToListAsync();
    public async Task<List<Reservation>> GetReservationsByDate(DateTime Date) => await _context.ReservationCollection.Find<Reservation>(c => c.DateOfReservation == Date).ToListAsync();
    public async Task<List<Reservation>> GetReservationsByDateAndHotel(DateTime Date, string HotelName) => await _context.ReservationCollection.Find<Reservation>(c => (c.Hotel.Name == HotelName) && (c.DateOfReservation == Date)).ToListAsync();
    public async Task<List<Reservation>> GetReservationsByNameAndDate(string Name, DateTime Date) => await _context.ReservationCollection.Find<Reservation>(c => (c.Name == Name) && (c.DateOfReservation == Date)).ToListAsync();
    public async Task<List<Reservation>> GetReservationsByNameAndHotel(string Name, string HotelName) => await _context.ReservationCollection.Find<Reservation>(c => (c.Name == Name) && (c.Hotel.Name == HotelName)).ToListAsync();
    public async Task<List<Reservation>> GetReservationsByNameAndHotelAndDate(string Name, string HotelName, DateTime Date) => await _context.ReservationCollection.Find<Reservation>(c => (c.Name == Name) && (c.Hotel.Name == HotelName) && (c.DateOfReservation == Date)).ToListAsync();

    // POST
    public async Task AddReservation(Reservation NewReservation) => await _context.ReservationCollection.InsertOneAsync(NewReservation);

    // PUT
    public async Task<Reservation> UpdateReservation(Reservation reservation)
    {
        try
        {
            var filter = Builders<Reservation>.Filter.Eq("Id", reservation.Id);
            var update = Builders<Reservation>.Update.Set("Name", reservation.Name).Set("FirstName", reservation.FirstName).Set("BirthDate", reservation.BirthDate).Set("EMail", reservation.EMail).Set("Hotel", reservation.Hotel).Set("NumberOfRooms", reservation.NumberOfRooms).Set("DateOfReservation", reservation.DateOfReservation).Set("Review", reservation.Review).Set("TotalPrice", reservation.TotalPrice);
            var result = await _context.ReservationCollection.UpdateOneAsync(filter, update);
            return await GetReservationById(reservation.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    //DELETE    
    public async Task DeleteReservation(string Id)
    {
        try
        {
            var filter = Builders<Reservation>.Filter.Eq("Id", Id);
            var result = await _context.ReservationCollection.DeleteOneAsync(filter);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

}
