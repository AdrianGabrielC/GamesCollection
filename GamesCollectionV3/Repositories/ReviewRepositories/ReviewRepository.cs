using GamesCollectionV3.Data;
using GamesCollectionV3.Models;
using Microsoft.EntityFrameworkCore;
using GamesCollectionV3.Utility;

namespace GamesCollectionV3.Repositories.ReviewRepositories
{
    // This class represents the repository for managing Review entities.
    // It extends the RepositoryBase class and implements the IReviewRepository interface.
    // The repository provides methods for interacting with Review entities in the database.
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(GamesCollectionV3Context context) : base(context)
        {

        }

        // Retrieves a review by its ID asynchronously.
        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await FindByCondition(review => review.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public void CreateReview(Review review)
        {
            Create(review);
        }
        public void UpdateReview(Review review)
        {
            Update(review);
        }
        public void DeleteReview(Review review)
        {
            Delete(review);
        }
    }
}
