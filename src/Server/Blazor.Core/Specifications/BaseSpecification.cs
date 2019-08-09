using Blazor.Core.Interfaces;
using System;
using System.Linq.Expressions;

namespace Blazor.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; }
        protected BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        //public List<Expression<Func<T, object>>> Includes { get; }
        //    = new List<Expression<Func<T, object>>>();
        //public Expression<Func<T, object>> OrderBy { get; private set; }
        //public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }

        protected virtual void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}
