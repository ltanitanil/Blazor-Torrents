using Blazor.Core.Entities;
using Blazor.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blazor.Infrastructure.Data.Repositories
{
    public class EFRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        private readonly CatalogContext _eFContext;

        public EFRepository(CatalogContext eFContext)
        {
            _eFContext = eFContext;
        }

        public Task<int> CountAsync(ISpecification<T> specification)
        {
            return ApplySpecification(specification).CountAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _eFContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        public IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_eFContext.Set<T>().AsQueryable(), spec);
        }

        public async Task<IReadOnlyList<int>> GetPopularEntriesAsync(int count, Expression<Func<T, int>> expression)
        {
            return await _eFContext.Set<T>().GroupBy(expression)
                                            .OrderByDescending(x => x.Count())
                                            .Take(count)
                                            .Select(x => x.Key)
                                            .ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetListByIDsAsync(IReadOnlyList<int> iDs)
        {
            return await _eFContext.Set<T>().Where(x => iDs.Any(z => z == x.Id)).ToListAsync();
        }

        public async Task<long> GetMaxValueAsync(Expression<Func<T, long>> expression)
        {
            return await _eFContext.Set<T>().MaxAsync(expression);
        }
    }
}
