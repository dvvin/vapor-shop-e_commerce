using System.Linq.Expressions;

namespace Core.Specifications
{
    // Create an interface named ISpecification with a generic type T
    public interface ISpecification<T>
    {
        // Declare a property Criteria of type Expression that represents a function that takes a T and returns a bool
        Expression<Func<T, bool>> Criteria { get; }

        // Declare a property Includes of type List that represents a list of functions that take a T and return an object
        List<Expression<Func<T, object>>> Includes { get; }
    }
}
