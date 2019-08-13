using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Server.DataAccessLayer.Data.Entities;


namespace Blazor.Server.DataAccessLayer.Data.Repositories
{
    public interface ITorrentsRepository : IEFRepository<Torrent>
    {
        Task<IReadOnlyList<Forum>> GetPopularForumsAsync(int count);
        Task<long> GetMaxTorrentSizeAsync();
    }
}
