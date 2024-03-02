using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    // Declare the generic repository interface, which takes a type parameter T that must inherit from BaseEntity
    public interface IGenericRepository<T>
        where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id); // Declare a method to asynchronously get an entity by its ID

        Task<IReadOnlyList<T>> ListAllAsync(); // Declare a method to asynchronously list all entities of type T

        // Declare a method to asynchronously get an entity with a specific specification
        Task<T> GetEntityWithSpec(ISpecification<T> spec);

        // Declare a method to asynchronously list entities that match a specific specification
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

        // Declare a method to asynchronously count the number of entities that match a specific specification
        Task<int> CountAsync(ISpecification<T> spec);
    }
}
