using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.BusinessLayer.Entities;
using Blazor.BusinessLayer.Interfaces;

namespace Blazor.DataAccessLayer.Data.Repositories
{
    public class EFRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly CatalogContext _eFContext;

        public EFRepository(CatalogContext eFContext)
        {
            _eFContext = eFContext;
        }

        public async Task<int> CountAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).CountAsync();
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
    }
}
