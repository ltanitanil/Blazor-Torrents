using Blazor.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.Core.Interfaces
{
    public interface ITorrentsRepository : IAsyncRepository<Torrent>
    {
        Task<IReadOnlyList<Forum>> GetPopularForumsAsync(int count);
        Task<long> GetMaxTorrentSizeAsync();
    }
}
