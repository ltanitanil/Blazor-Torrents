using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blazor.Server.DataAccessLayer.Data.Entities;
using Blazor.Server.DataAccessLayer.Data.Repositories;
using Blazor.Server.DataAccessLayer.Data.Specifications;

namespace Blazor.Server.BusinessLayer.Services.TorrentsService
{
    public class TorrentsService : ITorrentsService
    {
        private readonly ITorrentsRepository _torrentsRepository;

        public TorrentsService(ITorrentsRepository torrentsRepository)
        {
            _torrentsRepository = torrentsRepository;
        }

        public async Task<(IReadOnlyList<Forum>, long)> GetDataToFilter(int forumsCount) =>
            (await _torrentsRepository.GetPopularForumsAsync(forumsCount),
                await _torrentsRepository.GetMaxTorrentSizeAsync());

        public async Task<Torrent> GetTorrent(int id) => await _torrentsRepository.GetByIdAsync(id);

        public async Task<IReadOnlyList<Torrent>> GetTorrents(int skip, int take, string search, int? forumId, 
            long? sizeFrom, long? sizeTo, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
        {
            var specification = new TorrentsFilterPaginatedSpecification(skip, take, search, forumId, sizeFrom, sizeTo, dateFrom, dateTo);

            return await _torrentsRepository.ListAsync(specification);
        }

        public async Task<int> GetTorrentsCount(string search, int? forumId, long? sizeFrom, long? sizeTo, 
            DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
        {
            var specification = new TorrentsFilterSpecification(search, forumId, sizeFrom, sizeTo, dateFrom, dateTo);

            return await _torrentsRepository.CountAsync(specification);
        }
    }
}
