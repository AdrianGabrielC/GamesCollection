using GamesCollectionV3.Repositories.GameRepositories;
using GamesCollectionV3.Repositories.ReviewRepositories;
using GamesCollectionV3.Data;

namespace GamesCollectionV3.Repositories
{
    // This class implements the IRepositoryManager interface.
    // It serves as the repository manager responsible for providing access to different repositories and saving changes.
    public class RepositoryManager : IRepositoryManager
    {
        private GamesCollectionV3Context _repositoryContext;
        private IGameRepository _gameRepository;
        private IReviewRepository _reviewRepository;

        public RepositoryManager(GamesCollectionV3Context repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        // Property for accessing the Game repository.
        // It checks if the game repository instance is null, and if so, creates a new instance.
        public IGameRepository Game
        {
            get
            {
                if (_gameRepository == null)
                {
                    _gameRepository = new GameRepository(_repositoryContext);
                }
                return _gameRepository;
            }
        }

        // Property for accessing the Review repository.
        // It checks if the review repository instance is null, and if so, creates a new instance.
        public IReviewRepository Review
        {
            get
            {
                if (_reviewRepository == null)
                {
                    _reviewRepository = new ReviewRepository(_repositoryContext);
                }
                return _reviewRepository;
            }
        }

        // Asynchronously saves changes made to the repositories.
        public async Task SaveAsync()
        {
            await _repositoryContext.SaveChangesAsync();
        }
    }
}
