using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Server.DataAccessLayer.Data.Context;
using Blazor.Server.DataAccessLayer.Data.Entities;


namespace Blazor.Server.DataAccessLayer.Data.Repositories
{
    public class TorrentsRepository : EFRepository<Torrent>, ITorrentsRepository
    {
        public TorrentsRepository(TorrentsContext catalogContext) : base(catalogContext)
        {
        }

        public async Task<IReadOnlyList<Forum>> GetPopularForumsAsync(int count)
        {
            return await _dbSet
                .GroupBy(x => x.ForumId, (key, items) => new { Key = key, Count = items.Count() })
                .OrderByDescending(x => x.Count)
                .Take(count)
                .Join(_eFContext.Forums, (t) => t.Key, (f) => f.Id, (t, f) => f)
                .ToListAsync();
        }

        public async Task<long> GetMaxTorrentSizeAsync() =>
            await _dbSet.MaxAsync(x => x.Size);
    }
}
