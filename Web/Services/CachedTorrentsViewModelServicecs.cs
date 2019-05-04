using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Interfaces;
using Web.ViewModels;
using Web.ViewModels.Torrent;

namespace Web.Services
{
    public class CachedTorrentsViewModelService : ITorrentsViewModelService
    {
        private readonly IMemoryCache _cache;
        private readonly TorrentsViewModelService _torrentViewModelService;
        private static readonly string _torrentKeyTemplate = "torrent-{0}";
        private static readonly string _torrentsKeyTemplate = "torrents-{0}-{1}-{2}";
        private static readonly TimeSpan _defaultCacheDuration = TimeSpan.FromSeconds(30);

        public CachedTorrentsViewModelService(IMemoryCache cache, TorrentsViewModelService torrentViewModelService)
        {
            _cache = cache;
            _torrentViewModelService = torrentViewModelService;
        }

        public async Task<TorrentDescriptionViewModel> GetTorrent(int id)
        {
            string cacheKey = String.Format(_torrentKeyTemplate, id);
            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;
                return await _torrentViewModelService.GetTorrent(id);
            });
        }

        public async Task<TorrentsViewModel> GetTorrents(int pageIndex, int itemsPage, string selectedTitle)
        {
            string cacheKey = String.Format(_torrentsKeyTemplate, pageIndex, itemsPage, selectedTitle);
            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;
                return await _torrentViewModelService.GetTorrents(pageIndex, itemsPage, selectedTitle);
            });
        }
    }
}
