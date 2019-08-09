using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.BusinessLayer.Entities;

namespace Blazor.BusinessLayer.Interfaces
{
    public interface ITorrentsRepository : IAsyncRepository<Torrent>
    {
        Task<IReadOnlyList<Forum>> GetPopularForumsAsync(int count);
        Task<long> GetMaxTorrentSizeAsync();
    }
}
