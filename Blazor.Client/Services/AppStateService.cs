using Blazor.Shared.ViewModels;
using Blazor.Shared.ViewModels.Search;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blazor.Client.Services
{
    public class AppStateService
    {
        private readonly HttpClient _httpClient;

        public AppStateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TorrentsViewModel> GetTorrentsAsync(SearchAndFilterCriteria criteria, int? pageIndex)
        {
            return await _httpClient.PostJsonAsync<TorrentsViewModel>(string.Format("api/Torrents/GetTorrents/?pageIndex={0}", pageIndex), criteria);
        }

        public async Task<SearchAndFilterData> GetDataToFilter()
        {
            return await _httpClient.GetJsonAsync<SearchAndFilterData>("api/Torrents/GetDataToFilter");
        }


    }
}
