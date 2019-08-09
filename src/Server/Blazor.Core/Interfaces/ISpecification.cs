using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Blazor.BusinessLayer.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }

        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}
