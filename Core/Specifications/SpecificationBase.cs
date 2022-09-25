using System.Linq.Expressions;

namespace Core.Specifications
{
    public class SpecificationBase<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>>? Criteria { get; }

        public IList<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();


        public SpecificationBase()
        {
        }

        public SpecificationBase(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        protected void AddInclude(Expression<Func<T, object>> include) => Includes.Add(include);
    }
}