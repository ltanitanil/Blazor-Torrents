using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using Blazor.Server.BusinessLayer.Settings;
using Blazor.Server.DataAccessLayer.Entities;
using Microsoft.Extensions.Options;
using Blazor.Server.BusinessLayer.Extensions;
using Microsoft.AspNetCore.Http;

namespace Blazor.Server.BusinessLayer.Services.TorrentsService
{
    public class TorrentsServiceCacheDecorator : ITorrentsService
    {
        private readonly IMemoryCache _cache;
        private readonly TorrentsService _torrentsService;
        private readonly MemoryCacheEntryOptions _cacheEntryOptions;

        public TorrentsServiceCacheDecorator(IOptions<CacheOptionsSettings> cacheOptions,
            IMemoryCache cache, TorrentsService torrentsService)
        {
            _cache = cache;
            _torrentsService = torrentsService;
            _cacheEntryOptions = new MemoryCacheEntryOptions
            {
                SlidingExpiration = cacheOptions.Value?.SlidingExpiration
            };
        }

        public async Task<Torrent> GetTorrent(int id)
        {
            var cacheKey = $"torrent-{id}";

            return await _cache.GetOrCreateAsync(cacheKey,
                () => _torrentsService.GetTorrent(id), _cacheEntryOptions);
        }

        public async Task<(IReadOnlyList<Torrent>, int)> GetTorrentsAndCount(int pageIndex, int itemsPage, string search,
            int? forumId, long? sizeFrom, long? sizeTo, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
        {
            var cacheKey =
                $"torrents-{pageIndex}-{itemsPage}-{search}-{forumId}-{sizeFrom}-{sizeTo}-{dateFrom}-{dateTo}";

            return await _cache.GetOrCreateAsync(cacheKey,
                () => _torrentsService.GetTorrentsAndCount(pageIndex, itemsPage, search, forumId, sizeFrom, sizeTo, dateFrom, dateTo),
                _cacheEntryOptions);
        }

        public async Task<(IReadOnlyList<Forum>, long)> GetDataToFilter(int forumsCount)
        {
            var cacheKey = $"popularForums-{forumsCount}";

            return await _cache.GetOrCreateAsync(cacheKey, () => _torrentsService.GetDataToFilter(forumsCount), _cacheEntryOptions);
        }

        public async Task UploadTorrent(Torrent torrent, IEnumerable<IFormFile> files, string userName)
            => await _torrentsService.UploadTorrent(torrent, files, userName);
    }
}
