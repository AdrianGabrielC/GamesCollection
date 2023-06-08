using GamesCollectionV3.Models;
using GamesCollectionV3.Data;
using Microsoft.EntityFrameworkCore;
using GamesCollectionV3.Utility;

namespace GamesCollectionV3.Repositories.GameRepositories
{
    // This class represents the repository for managing Game entities.
    // It extends the RepositoryBase class and implements the IGameRepository interface.
    // The repository provides various methods for interacting with the Game entities in the database.
    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        public GameRepository(GamesCollectionV3Context context) : base(context)
        {
        }

        // Retrieves all games asynchronously.
        public async Task<IEnumerable<Game>> GetAllGamesAsync()
        {
            return await FindAll()
                .OrderBy(game => game.Title)
                .ToListAsync();
        }

        // Retrieves a specific page of games asynchronously based on the provided page size and number.
        public async Task<IEnumerable<Game>> GetGamePage(int pageSize, int pageNumber)
        {
            return await PaginatedList<Game>.CreateAsync(FindAll(), pageNumber, pageSize);
        }

        // Retrieves a game by its ID asynchronously.
        public async Task<Game> GetGameByIdAsync(int id)
        {
            return await FindByCondition(game => game.Id.Equals(id)).FirstOrDefaultAsync();
        }

        // Retrieves a game by its ID asynchronously along with its associated reviews.
        public async Task<Game> GetGameWithReviewsAsync(int id)
        {
            return await FindByCondition(game => game.Id.Equals(id))
                .Include(game => game.Reviews)
                .FirstOrDefaultAsync();
        }

        public void CreateGame(Game game)
        {
            Create(game);
        }

        public void UpdateGame(Game game)
        {
            Update(game);
        }

        public void DeleteGame(Game game)
        {
            Delete(game);
        }
    }
}
