using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Server.BusinessLayer.Entities;
using Blazor.Server.BusinessLayer.Interfaces;

namespace Blazor.Server.DataAccessLayer.Data.Repositories
{
    public class TorrentsRepository : EFRepository<Torrent>, ITorrentsRepository
    {
        public TorrentsRepository(CatalogContext catalogContext) : base(catalogContext)
        {
        }

        public async Task<IReadOnlyList<Forum>> GetPopularForumsAsync(int count)
        {
            return await _eFContext.Torrents
                .GroupBy(x => x.ForumId, (key, items) => new { Key = key, Count = items.Count() })
                .OrderByDescending(x => x.Count)
                .Take(count)
                .Join(_eFContext.Forums, (t) => t.Key, (f) => f.Id, (t, f) => f)
                .ToListAsync();
        }

        public async Task<long> GetMaxTorrentSizeAsync() =>
            await _eFContext.Torrents.MaxAsync(x => x.Size);
    }
}
