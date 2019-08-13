using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blazor.Server.DataAccessLayer.Data.Entities;

namespace Blazor.Server.BusinessLayer.Services.TorrentsService
{
    public interface ITorrentsService
    {
        Task<Torrent> GetTorrent(int id);
        Task<IReadOnlyList<Torrent>> GetTorrents(int skip, int take, string search, int? forumId, long? sizeFrom, long? sizeTo, DateTimeOffset? dateFrom, DateTimeOffset? dateTo);
        Task<int> GetTorrentsCount(string search, int? forumId, long? sizeFrom, long? sizeTo, DateTimeOffset? dateFrom, DateTimeOffset? dateTo);
        Task<(IReadOnlyList<Forum>, long)> GetDataToFilter(int forumsCount);
    }
}
