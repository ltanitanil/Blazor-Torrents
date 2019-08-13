using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Server.DataAccessLayer.Data.Context;
using Blazor.Server.DataAccessLayer.Data.Entities;
using Blazor.Server.DataAccessLayer.Data.Specifications;

namespace Blazor.Server.DataAccessLayer.Data.Repositories
{
    public class EFRepository<T> : IEFRepository<T> where T : BaseEntity
    {
        protected readonly TorrentsContext _eFContext;
        protected readonly DbSet<T> _dbSet;

        public EFRepository(TorrentsContext eFContext)
        {
            _eFContext = eFContext;
            _dbSet = _eFContext.Set<T>();
        }

        public async Task<int> CountAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).CountAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        public IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(), spec);
        }

    }
}
