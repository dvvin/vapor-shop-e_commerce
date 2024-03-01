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

        // Method to add include expressions to the list
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}
