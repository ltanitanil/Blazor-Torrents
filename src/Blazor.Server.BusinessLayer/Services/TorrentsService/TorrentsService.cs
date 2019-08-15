using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazor.Server.BusinessLayer.Helpers;
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

        public async Task<(IReadOnlyList<Forum>, long)> GetDataToFilter(int forumsCount)
        {
            return (await _torrentsRepository.GetPopularForumsAsync(forumsCount),
                await _torrentsRepository.GetMaxTorrentSizeAsync());
        }

        public async Task<Torrent> GetTorrent(int id)
        {
            var torrent = await _torrentsRepository.GetByIdAsync(id);
            torrent.Content=BBCodeToHtmlConverter.Format(torrent.Content);
            return torrent;
        } 

        public async Task<(IReadOnlyList<Torrent>,int)> GetTorrentsAndCount(int pageIndex, int itemsPage, string search, int? forumId, 
            long? sizeFrom, long? sizeTo, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
        {
            var filterPaginatedSpecification = new TorrentsFilterPaginatedSpecification(pageIndex*itemsPage, itemsPage, 
                                                   search, forumId, sizeFrom, sizeTo, dateFrom, dateTo);
            var filterSpecification = new TorrentsFilterSpecification(search, forumId, sizeFrom, sizeTo, dateFrom, dateTo);

            var torrents = await _torrentsRepository.ListAsync(filterPaginatedSpecification);
            var count = await _torrentsRepository.CountAsync(filterSpecification);

            return (torrents,count);
        }
    }
}
