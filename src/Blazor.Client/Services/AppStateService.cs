using Blazor.Client.Interfaces;
using Blazor.Shared.ViewModels;
using Blazor.Shared.ViewModels.Search;
using Blazor.Shared.ViewModels.TorrentModel;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blazor.Client.Services
{
    public class AppStateService : IAppStateService
    {
        private readonly HttpClient _httpClient;

        public AppStateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TorrentsViewModel> GetTorrentsAsync(SearchAndFilterCriteria criteria, int? pageIndex)
        {
            return await _httpClient.PostJsonAsync<TorrentsViewModel>($"api/Torrents/GetTorrents/?pageIndex={pageIndex}", criteria);
        }

        public async Task<SearchAndFilterData> GetDataToFilter()
        {
            return await _httpClient.GetJsonAsync<SearchAndFilterData>("api/Torrents/GetDataToFilter");
        }

        public async Task<TorrentDescriptionView> GetTorrentDescription(int id)
        {
            return await _httpClient.GetJsonAsync<TorrentDescriptionView>($"api/Torrents/GetTorrent/?id={id}");
        }


    }
}
