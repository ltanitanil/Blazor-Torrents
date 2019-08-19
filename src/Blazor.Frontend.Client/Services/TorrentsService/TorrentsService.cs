﻿using Blazor.Shared.ViewModels;
using Blazor.Shared.ViewModels.Search;
using Blazor.Shared.ViewModels.TorrentModel;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blazor.Frontend.Client.Services.TorrentsService
{
    public class TorrentsService : ITorrentsService
    {
        private readonly HttpClient _httpClient;

        public TorrentsService(HttpClient httpClient)
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