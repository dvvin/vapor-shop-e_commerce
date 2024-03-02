using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    // This class represents a generic specification evaluator for entities.
    // It is used to apply specifications to entity queries.
    public class SpecificationEvaluator<TEntity>
        where TEntity : BaseEntity
    {
        // This method applies the given specification to the input query and returns the modified query.
        public static IQueryable<TEntity> GetQuery(
            IQueryable<TEntity> inputQuery, // The input query to which the specification will be applied
            ISpecification<TEntity> spec // The specification to be applied to the input query
        )
        {
            var query = inputQuery; // Set the initial query as the input query

            if (spec.Criteria != null) // Check if the specification has criteria defined
            {
                query = query.Where(spec.Criteria); // Apply the criteria to the query
            }

            if (spec.OrderBy != null) // Check if the specification has an order by defined
            {
                query = query.OrderBy(spec.OrderBy); // Apply the order by to the query
            }

            if (spec.OrderByDescending != null) // Check if the specification has an order by descending defined
            {
                query = query.OrderByDescending(spec.OrderByDescending); // Apply the order by descending to the query
            }

            if (spec.IsPagingEnabled) // Check if paging is enabled
            {
                query = query.Skip(spec.Skip).Take(spec.Take); // Apply paging
            }

            // Apply all included entities specified in the specification to the query
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query; // Return the modified query
        }
    }
}
