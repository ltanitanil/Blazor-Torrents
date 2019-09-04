using Blazor.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Blazor.Shared.ViewModels.Search;
using Blazor.Shared.ViewModels.TorrentModel;
using Microsoft.AspNetCore.Components;

namespace Blazor.Frontend.BusinessLayer.Services.TorrentsService
{
    public class TorrentsService : ITorrentsService
    {
        private readonly HttpClient _httpClient;

        public TorrentsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TorrentsViewModel> GetTorrentsAsync(SearchAndFilterCriteria criteria, int? pageIndex) => 
            await _httpClient.PostJsonAsync<TorrentsViewModel>($"api/Torrents/GetTorrents/?pageIndex={pageIndex}", criteria);

        public async Task<SearchAndFilterData> GetDataToFilter() => 
            await _httpClient.GetJsonAsync<SearchAndFilterData>("api/Torrents/GetDataToFilter");

        public async Task<TorrentDescriptionView> GetTorrentDescription(int id) => 
            await _httpClient.GetJsonAsync<TorrentDescriptionView>($"api/Torrents/GetTorrent/?id={id}");
    }
}
