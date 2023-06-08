using GamesCollectionV3.Models;


namespace GamesCollectionV3.Repositories.ReviewRepositories
{
    // This interface represents the contract for a Review repository.
    // It extends the IRepositoryBase interface and defines additional methods specific to Review entities.
    public interface IReviewRepository : IRepositoryBase<Review>
    {
        // Retrieves a review by its ID asynchronously.
        Task<Review> GetReviewByIdAsync(int id);
        void CreateReview(Review review);
        void UpdateReview(Review review);
        void DeleteReview(Review review);
    }
}

