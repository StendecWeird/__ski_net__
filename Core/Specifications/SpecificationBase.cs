using System.Linq.Expressions;

namespace Core.Specifications
{
    public class SpecificationBase<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>>? Criteria { get; }

        public IList<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>>? OrderBy { get; private set; }

        public Expression<Func<T, object>>? OrderByDescending { get; private set; }

        public int Skip { get; private set; }
        public int Take { get; private set; }
        public bool IsPagingEnabled { get; private set; }

        public SpecificationBase()
        {
        }

        public SpecificationBase(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        protected void AddInclude(Expression<Func<T, object>> include) => Includes.Add(include);

        protected void SetOrderBy(Expression<Func<T, object>> orderBy) => OrderBy = orderBy;
        protected void SetOrderByDescending(Expression<Func<T, object>> orderByDesc) => OrderByDescending = orderByDesc;
        protected void AddPagination(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}