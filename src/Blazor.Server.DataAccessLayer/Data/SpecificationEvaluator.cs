using System.Linq;
using Blazor.Server.DataAccessLayer.Data.Entities;
using Blazor.Server.DataAccessLayer.Data.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.DataAccessLayer.Data
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            if (specification == null)
                return inputQuery;

            var query = inputQuery;

            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }
            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip)
                             .Take(specification.Take);
            }
            return query;
        }
    }
}
