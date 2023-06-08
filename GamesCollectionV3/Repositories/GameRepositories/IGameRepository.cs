using GamesCollectionV3.Models;


namespace GamesCollectionV3.Repositories.GameRepositories
{
    // This interface represents the contract for a Game repository.
    // It extends the IRepositoryBase interface and defines additional methods specific to Game entities.
    public interface IGameRepository : IRepositoryBase<Game>
    {
        // Retrieves all games asynchronously.
        Task<IEnumerable<Game>> GetAllGamesAsync();

        // Retrieves a specific page of games asynchronously based on the provided page size and number.
        Task<IEnumerable<Game>> GetGamePage(int pageSize, int pageNumber);

        // Retrieves a game by its ID asynchronously.
        Task<Game> GetGameByIdAsync(int id);

        // Retrieves a game by its ID asynchronously along with its associated reviews.
        Task<Game> GetGameWithReviewsAsync(int id);
        void CreateGame(Game game);
        void UpdateGame(Game game);
        void DeleteGame(Game game);
    }
}
