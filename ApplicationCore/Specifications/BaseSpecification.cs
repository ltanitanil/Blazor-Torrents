using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.Specifications
{
    public class BaseSpecification<T>:ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes => new List<Expression<Func<T, object>>>();

        protected BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
    }
}
