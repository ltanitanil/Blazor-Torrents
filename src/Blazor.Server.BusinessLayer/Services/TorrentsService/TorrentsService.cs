using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Blazor.Server.BusinessLayer.Exceptions;
using Blazor.Server.BusinessLayer.Helpers;
using Blazor.Server.DataAccessLayer.Repositories;
using Blazor.Server.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

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
                await _torrentsRepository.MaxAsync(x => x.Size));
        }

        public async Task<Torrent> GetTorrent(int id)
        {
            if (id < 0)
                throw new AppException(ExceptionEvent.InvalidParameters, "Id can't be negative");

            var torrent = await _torrentsRepository.SingleAsync(expression: x => x.Id.Equals(id),
                              includeProperties: new List<Expression<Func<Torrent, object>>>
                              {
                                  x => x.Files,
                                  x => x.Forum,
                              }) ?? throw new AppException(ExceptionEvent.NotFound, $"Torrent(id={id}) not found");

            torrent.Content = BBCodeToHtmlConverter.Format(torrent.Content);

            return torrent;
        }

        public async Task<(IReadOnlyList<Torrent>, int)> GetTorrentsAndCount(int pageIndex, int itemsPage, string search,
            int? forumId, long? sizeFrom, long? sizeTo, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
        {
            if (pageIndex < 0)
                throw new AppException(ExceptionEvent.InvalidParameters, "Page can't be negative");

            Expression<Func<Torrent, bool>> filter = x => (string.IsNullOrEmpty(search) || x.Title.Contains(search))
                                                          && (!forumId.HasValue || x.ForumId == forumId)
                                                          && (!sizeFrom.HasValue || x.Size >= sizeFrom)
                                                          && (!sizeTo.HasValue || x.Size <= sizeTo)
                                                          && (!dateFrom.HasValue || x.RegisteredAt >= dateFrom)
                                                          && (!dateTo.HasValue || x.RegisteredAt <= dateTo);

            var torrents = await _torrentsRepository.GetAll(filter)
                               .Skip(pageIndex * itemsPage)
                               .Take(itemsPage)
                               .ToListAsync() ?? throw new AppException(ExceptionEvent.NotFound, $"Torrents not found");

            var count = await _torrentsRepository.GetAll(filter).CountAsync();

            return (torrents, count);
        }
    }
}
