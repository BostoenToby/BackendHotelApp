var builder = WebApplication.CreateBuilder(args);

var mongoSettings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(mongoSettings);
builder.Services.AddTransient<IMongoContext, MongoContext>();

builder.Services.AddTransient<IHotelRepository, HotelRepository>();
builder.Services.AddTransient<IRoomTypeRepository, RoomTypeRepository>();
builder.Services.AddTransient<IReservationRepository, ReservationRepository>();
builder.Services.AddTransient<IReviewRepository, ReviewRepository>();
builder.Services.AddTransient<IHotelService, HotelService>();

var app = builder.Build();
// app.MapGraphQL();

app.MapGet("/", () => "Hello World!");

app.MapGet("/hotels", async(IHotelService hotelService) => await hotelService.GetAllHotels());
app.MapGet("/hotel/{Id}", async(IHotelService hotelService, string Id) => await hotelService.GetHotelById(Id));
app.MapGet("/hotels/{NamePiece}", async(IHotelService hotelService, string NamePiece) => await hotelService.GetHotelsByNamePiece(NamePiece));
app.MapGet("/roomtypes", async(IHotelService hotelService) => await hotelService.GetAllRoomTypes());
app.MapGet("/roomtype/{Id}", async(IHotelService hotelService, string Id) => await hotelService.GetRoomTypeById(Id));
app.MapGet("/roomtypes/{NamePiece}", async(IHotelService hotelService, string NamePiece) => await hotelService.GetRoomTypesByNamePiece(NamePiece));
app.MapGet("/reservations", async(IHotelService hotelService) => await hotelService.GetAllReservations());
app.MapGet("/reservations/Name={Name}&FirstName={FirstName}", async(IHotelService hotelService, string Name, string FirstName) => await hotelService.GetReservationsByName(Name, FirstName));
app.MapGet("/reservation/{Id}",async(IHotelService hotelService, string Id) => await hotelService.GetReservationById(Id));
app.MapGet("/reviews", async(IHotelService hotelService) => await hotelService.GetAllReviews());
app.MapGet("/reviews/{Author}", async(IHotelService hotelService, string Author) => await hotelService.GetReviewsByAuthor(Author));
app.MapGet("/review/{Id}", async(IHotelService hotelService, string Id) => await hotelService.GetReviewById(Id));

app.MapPost("/hotel", async(IHotelService hotelService, Hotel hotel) => await hotelService.AddHotel(hotel));
app.MapPost("/roomtype", async(IHotelService hotelService, RoomType roomType) => await hotelService.AddRoomType(roomType));
app.MapPost("/reservation", async(IHotelService hotelService, Reservation reservation) => await hotelService.AddReservation(reservation));
app.MapPost("/review", async(IHotelService hotelService, Review review) => await hotelService.AddReview(review));

app.MapPut("/hotel/{Id}", async(IHotelService hotelService, Hotel hotel, string Id) => await hotelService.UpdateHotel(hotel, Id));
app.MapPut("/roomtype/{Id}", async(IHotelService hotelService, RoomType roomType, string Id) => await hotelService.UpdateRoomType(roomType, Id));
app.MapPut("/reservation/{Id}", async(IHotelService hotelService, Reservation reservation, string Id) => await hotelService.UpdateReservation(reservation, Id));
app.MapPut("/review/{Id}", async(IHotelService hotelService, Review review, string Id) => await hotelService.UpdateReview(review, Id));

app.MapDelete("/hotel/{Id}", async(IHotelService hotelService, string Id) => await hotelService.DeleteHotel(Id));
app.MapDelete("/roomtype/{Id}", async(IHotelService hotelService, string Id) => await hotelService.DeleteRoomType(Id));
app.MapDelete("/reservation/{Id}", async(IHotelService hotelService, string Id) => await hotelService.DeleteReservation(Id));
app.MapDelete("/review/{Id}", async(IHotelService hotelService, string Id) => await hotelService.DeleteReview(Id));

app.Run("https://0.0.0.0:3000");
