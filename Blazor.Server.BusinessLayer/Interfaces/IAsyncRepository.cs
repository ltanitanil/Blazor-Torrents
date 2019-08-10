using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Server.BusinessLayer.Entities;

namespace Blazor.Server.BusinessLayer.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification);
        Task<int> CountAsync(ISpecification<T> specification);
    }
}
