using GamesCollectionV3.Repositories.GameRepositories;
using GamesCollectionV3.Repositories.ReviewRepositories;

namespace GamesCollectionV3.Repositories
{
    // This interface represents the contract for the repository manager.
    // It defines properties for accessing different repositories and a method for saving changes.
    public interface IRepositoryManager
    {
        IGameRepository Game { get; }
        IReviewRepository Review { get; }

        Task SaveAsync();
    }
}
