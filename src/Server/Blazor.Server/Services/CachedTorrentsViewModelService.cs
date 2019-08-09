using Blazor.Server.Interfaces;
using Blazor.Shared.ViewModels;
using Blazor.Shared.ViewModels.Search;
using Blazor.Shared.ViewModels.TorrentModel;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using Blazor.Server.Settings;
using Microsoft.Extensions.Options;
using Blazor.Server.Extensions;

namespace Blazor.Server.Services
{
    public class CachedTorrentsViewModelService : ITorrentsViewModelService
    {
        private readonly IMemoryCache _cache;
        private readonly TorrentsViewModelService _torrentViewModelService;
        private readonly MemoryCacheEntryOptions _cacheEntryOptions;

        public CachedTorrentsViewModelService(IOptions<CacheOptionsSettings> cacheOptions, IMemoryCache cache, TorrentsViewModelService torrentViewModelService)
        {
            _cache = cache;
            _torrentViewModelService = torrentViewModelService;
            _cacheEntryOptions = new MemoryCacheEntryOptions { SlidingExpiration = cacheOptions.Value?.SlidingExpiration };
        }

        public async Task<TorrentDescriptionView> GetTorrent(int id)
        {
            var cacheKey = $"torrent-{id}";

            return await _cache.GetOrCreateAsync(cacheKey,
                () => _torrentViewModelService.GetTorrent(id), _cacheEntryOptions);
        }

        public async Task<TorrentsViewModel> GetTorrents(int pageIndex, int itemsPage, SearchAndFilterCriteria criteria)
        {
            var cacheKey =
                $"torrents-{pageIndex}-{itemsPage}-{criteria.SearchText}-{criteria.SelectedForumId}-{criteria.Size.From}-{criteria.Size.To}-{criteria.Date.From}-{criteria.Date.To}";

            return await _cache.GetOrCreateAsync(cacheKey,
                () => _torrentViewModelService.GetTorrents(pageIndex, itemsPage, criteria), _cacheEntryOptions);
        }

        public async Task<SearchAndFilterData> GetDataToFilter(int forumsCount)
        {
            var cacheKey = $"popularForums-{forumsCount}";

            return await _cache.GetOrCreateAsync(cacheKey, () => _torrentViewModelService.GetDataToFilter(forumsCount), _cacheEntryOptions);
        }

    }
}
