using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Server.BusinessLayer.Entities;

namespace Blazor.Server.BusinessLayer.Interfaces
{
    public interface ITorrentsRepository : IAsyncRepository<Torrent>
    {
        Task<IReadOnlyList<Forum>> GetPopularForumsAsync(int count);
        Task<long> GetMaxTorrentSizeAsync();
    }
}
