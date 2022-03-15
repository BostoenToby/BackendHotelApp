namespace Hotels.Repositories;

public interface IReviewRepository
{
    Task<Review> AddReview(Review review);
    Task DeleteReview(string Id);
    Task<List<Review>> GetAllReviews();
    Task<Review> GetReviewById(string Id);
    Task<List<Review>> GetReviewsByAuthor(string Author);
    Task<Review> UpdateReview(Review review);
}

public class ReviewRepository : IReviewRepository
{
    private readonly IMongoContext _context;

    public ReviewRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<List<Review>> GetAllReviews() => await _context.ReviewsCollection.Find<Review>(_ => true).ToListAsync();
    public async Task<Review> GetReviewById(string Id) => await _context.ReviewsCollection.Find<Review>(c => c.Id == Id).FirstOrDefaultAsync();
    public async Task<List<Review>> GetReviewsByAuthor(string Author) => await _context.ReviewsCollection.Find<Review>(c => c.Author == Author).ToListAsync();
    public async Task<Review> AddReview(Review review)
    {
        await _context.ReviewsCollection.InsertOneAsync(review);
        return review;
    }
    public async Task<Review> UpdateReview(Review review)
    {
        try
        {
            var filter = Builders<Review>.Filter.Eq("Id", review.Id);
            var update = Builders<Review>.Update.Set("Author", review.Author).Set("Image", review.Image).Set("StarRating", review.StarRating).Set("ReviewDescription", review.ReviewDescription);
            var result = await _context.ReviewsCollection.UpdateOneAsync(filter, update);
            return await GetReviewById(review.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
    public async Task DeleteReview(string Id)
    {
        try
        {
            var filter = Builders<Review>.Filter.Eq("Id", Id);
            var result = await _context.ReviewsCollection.DeleteOneAsync(filter);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}