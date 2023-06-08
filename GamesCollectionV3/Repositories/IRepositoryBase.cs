using System.Linq.Expressions;

namespace GamesCollectionV3.Repositories
{
    // This interface represents the base contract for all repositories.
    // It defines generic methods for common repository operations.
    public interface IRepositoryBase<T>
    {
        // Retrieves all entities of type T.
        IQueryable<T> FindAll();

        // Retrieves entities of type T based on the provided condition.
        // Takes an expression parameter representing the condition.
        // Returns an IQueryable<T> representing the filtered collection of entities.
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
