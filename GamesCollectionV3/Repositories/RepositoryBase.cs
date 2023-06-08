using GamesCollectionV3.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace GamesCollectionV3.Repositories
{
    // This abstract class serves as the base implementation for repositories.
    // It implements the IRepositoryBase interface with generic methods for common repository operations.
    // The class is parameterized with type T, which represents the entity type for the repository.
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected GamesCollectionV3Context _context;

        public RepositoryBase(GamesCollectionV3Context context)
        {
            _context = context;
        }

        // Retrieves all entities of type T.
        public IQueryable<T> FindAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        // Retrieves entities of type T based on the provided condition.
        // Takes an expression parameter representing the condition.
        // Returns an IQueryable<T> representing the filtered collection of entities.
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }

}
