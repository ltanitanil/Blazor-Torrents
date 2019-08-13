using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Blazor.Server.DataAccessLayer.Data.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }

        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}
