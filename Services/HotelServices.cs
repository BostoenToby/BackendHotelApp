namespace Hotels.HotelService;

public interface IHotelService
{
    Task AddHotel(Hotel newHotel);
    Task AddReservation(Reservation newReservation);
    Task AddReview(Review newReview);
    Task AddRoomType(RoomType newRoomType);
    Task DeleteHotel(string Id);
    Task DeleteReservation(string Id);
    Task DeleteReview(string Id);
    Task DeleteRoomType(string Id);
    Task<List<Hotel>> GetAllHotels();
    Task<List<Reservation>> GetAllReservations();
    Task<List<Review>> GetAllReviews();
    Task<List<RoomType>> GetAllRoomTypes();
    Task<List<Hotel>> GetHotelByCity(string City);
    Task<List<Hotel>> GetHotelByCityAndStars(int Stars, string City);
    Task<Hotel> GetHotelById(string Id);
    Task<Hotel> GetHotelByName(string Name);
    Task<List<Hotel>> GetHotelByProvince(string Province);
    Task<List<Hotel>> GetHotelByProvinceAndStars(int Stars, string Province);
    Task<List<Hotel>> GetHotelByStars(int Stars);
    Task<List<Reservation>> GetReservationsByDate(DateTime Date);
    Task<List<Reservation>> GetReservationsByDateAndHotel(DateTime Date, string HotelName);
    Task<List<Reservation>> GetReservationsByHotel(string HotelName);
    Task<List<Reservation>> GetReservationsByName(string Name);
    Task<List<Reservation>> GetReservationsByNameAndDate(string Name, DateTime Date);
    Task<List<Reservation>> GetReservationsByNameAndHotel(string Name, string HotelName);
    Task<List<Reservation>> GetReservationsByNameAndHotelAndDate(string Name, string HotelName, DateTime Date);
    Task<Reservation> GetReversationById(string Id);
    Task<Review> GetReviewById(string Id);
    Task<List<Review>> GetReviewsByAuthor(string Author);
    Task<RoomType> GetRoomTypeById(string Id);
    Task<RoomType> GetRoomTypeByName(string Name);
    Task<List<RoomType>> GetRoomTypesByNumberOfBeds(int NumberOfBeds);
    Task<Hotel> UpdateHotel(Hotel hotel);
    Task<Reservation> UpdateReservation(Reservation reservation);
    Task<Review> UpdateReview(Review review);
    Task<RoomType> UpdateRoomType(RoomType roomType);
}

public class HotelService : IHotelService
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IRoomTypeRepository _roomTypeRepository;
    public HotelService(IHotelRepository hotelRepository, IReservationRepository reservationRepository, IReviewRepository reviewRepository, IRoomTypeRepository roomTypeRepository)
    {
        _hotelRepository = hotelRepository;
        _reservationRepository = reservationRepository;
        _reviewRepository = reviewRepository;
        _roomTypeRepository = roomTypeRepository;
    }
    // Hotel
    public async Task<List<Hotel>> GetAllHotels() => await _hotelRepository.GetAllHotels();
    public async Task<Hotel> GetHotelById(string Id) => await _hotelRepository.GetHotelById(Id);
    public async Task<Hotel> GetHotelByName(string Name) => await _hotelRepository.GetHotelByName(Name);
    public async Task<List<Hotel>> GetHotelByCity(string City) => await _hotelRepository.GetHotelsByCity(City);
    public async Task<List<Hotel>> GetHotelByProvince(string Province) => await _hotelRepository.GetHotelsByProvince(Province);
    public async Task<List<Hotel>> GetHotelByStars(int Stars) => await _hotelRepository.GetHotelsByStars(Stars);
    public async Task<List<Hotel>> GetHotelByProvinceAndStars(int Stars, string Province) => await _hotelRepository.GetHotelsByProvinceAndStars(Province, Stars);
    public async Task<List<Hotel>> GetHotelByCityAndStars(int Stars, string City) => await _hotelRepository.GetHotelsByCityAndStars(City, Stars);
    public async Task AddHotel(Hotel newHotel) => await _hotelRepository.AddHotel(newHotel);
    public async Task<Hotel> UpdateHotel(Hotel hotel) => await _hotelRepository.UpdateHotel(hotel);
    public async Task DeleteHotel(string Id) => await _hotelRepository.DeleteHotel(Id);

    // Reservation
    public async Task<List<Reservation>> GetAllReservations() => await _reservationRepository.GetAllReservations();
    public async Task<Reservation> GetReversationById(string Id) => await _reservationRepository.GetReservationById(Id);
    public async Task<List<Reservation>> GetReservationsByHotel(string HotelName) => await _reservationRepository.GetReservationsByHotel(HotelName);
    public async Task<List<Reservation>> GetReservationsByName(string Name) => await _reservationRepository.GetReservationsByName(Name);
    public async Task<List<Reservation>> GetReservationsByDate(DateTime Date) => await _reservationRepository.GetReservationsByDate(Date);
    public async Task<List<Reservation>> GetReservationsByDateAndHotel(DateTime Date, string HotelName) => await _reservationRepository.GetReservationsByDateAndHotel(Date, HotelName);
    public async Task<List<Reservation>> GetReservationsByNameAndDate(string Name, DateTime Date) => await _reservationRepository.GetReservationsByNameAndDate(Name, Date);
    public async Task<List<Reservation>> GetReservationsByNameAndHotel(string Name, string HotelName) => await _reservationRepository.GetReservationsByNameAndHotel(Name, HotelName);
    public async Task<List<Reservation>> GetReservationsByNameAndHotelAndDate(string Name, string HotelName, DateTime Date) => await _reservationRepository.GetReservationsByNameAndHotelAndDate(Name, HotelName, Date);
    public async Task AddReservation(Reservation newReservation) => await _reservationRepository.AddReservation(newReservation);
    public async Task<Reservation> UpdateReservation(Reservation reservation) => await _reservationRepository.UpdateReservation(reservation);
    public async Task DeleteReservation(string Id) => await _reservationRepository.DeleteReservation(Id);

    // Review
    public async Task<List<Review>> GetAllReviews() => await _reviewRepository.GetAllReviews();
    public async Task<Review> GetReviewById(string Id) => await _reviewRepository.GetReviewById(Id);
    public async Task<List<Review>> GetReviewsByAuthor(string Author) => await _reviewRepository.GetReviewsByAuthor(Author);
    public async Task AddReview(Review newReview) => await _reviewRepository.AddReview(newReview);
    public async Task<Review> UpdateReview(Review review) => await _reviewRepository.UpdateReview(review);
    public async Task DeleteReview(string Id) => await _reviewRepository.DeleteReview(Id);

    // RoomType
    public async Task<List<RoomType>> GetAllRoomTypes() => await _roomTypeRepository.GetAllRoomTypes();
    public async Task<List<RoomType>> GetRoomTypesByNumberOfBeds(int NumberOfBeds) => await _roomTypeRepository.GetRoomTypesByNumberOfBeds(NumberOfBeds);
    public async Task<RoomType> GetRoomTypeById(string Id) => await _roomTypeRepository.GetRoomTypeById(Id);
    public async Task<RoomType> GetRoomTypeByName(string Name) => await _roomTypeRepository.GetRoomTypeByName(Name);
    public async Task AddRoomType(RoomType newRoomType) => await _roomTypeRepository.AddRoomType(newRoomType);
    public async Task<RoomType> UpdateRoomType(RoomType roomType) => await _roomTypeRepository.UpdateRoomType(roomType);
    public async Task DeleteRoomType(string Id) => await _roomTypeRepository.DeleteRoomType(Id);
}