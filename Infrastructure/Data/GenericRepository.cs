using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    // This class represents a generic repository for entities of type T.
    // It implements the IGenericRepository interface, which defines the basic CRUD operations.
    public class GenericRepository<T>(StoreContext context) : IGenericRepository<T>
        where T : BaseEntity
    {
        private readonly StoreContext _context = context; // The context used for interacting with the database.

        // Retrieves an entity by its id asynchronously.
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        // Retrieves all entities of type T from the database asynchronously.
        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        // Retrieves a single entity of type T based on the provided specification asynchronously.
        // If no entity is found, it throws an exception.
        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            var result = await ApplySpecification(spec).FirstOrDefaultAsync();
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception("No entity found matching the specification.");
            }
        }

        // Retrieves a list of entities of type T based on the provided specification asynchronously.
        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        // Applies the given specification to the queryable set of entities of type T.
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}
