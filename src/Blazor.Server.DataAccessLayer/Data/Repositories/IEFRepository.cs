using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Server.DataAccessLayer.Data.Entities;
using Blazor.Server.DataAccessLayer.Data.Specifications;

namespace Blazor.Server.DataAccessLayer.Data.Repositories
{
    public interface IEFRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification);
        Task<int> CountAsync(ISpecification<T> specification);
    }
}
