using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification);
        Task<IReadOnlyList<int>> GetPopularEntriesAsync(int count, Expression<Func<T, int>> expression);
        Task<IReadOnlyList<T>> GetListByIDsAsync(IReadOnlyList<int> iDs);
        Task<int> CountAsync(ISpecification<T> specification);
        Task<long> GetMaxValueAsync(Expression<Func<T, long>> expression);
    }
}
