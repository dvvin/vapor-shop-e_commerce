using System.Linq.Expressions;

namespace Core.Specifications
{
    // Generic class for creating specifications
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification() { } // Default constructor

        public BaseSpecification(Expression<Func<T, bool>> criteria) // Constructor with criteria
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; } // Property to hold the criteria for the specification

        // List to store include expressions
        public List<Expression<Func<T, object>>> Includes { get; } =
            new List<Expression<Func<T, object>>>();

        // Properties to store order by and order by descending expressions
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        // Method to add include expressions to the list
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        // Methods to add order by and order by descending expressions
        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }

        public int Take { get; private set; } // Property to hold the number of entities to return

        public int Skip { get; private set; } // Property to hold the number of entities to skip

        public bool IsPagingEnabled { get; private set; } // Property to indicate if paging is enabled

        // Method to apply paging
        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}
