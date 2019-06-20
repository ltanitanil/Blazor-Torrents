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

        public TorrentsViewModel TorrentsViewModel { get; set; }
        public bool SearchInProgress { get; private set; }
        public event Action OnChange;

        public AppStateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task GetTorrentsAsync(SearchAndFilterCriteria criteria, int? pageIndex)
        {
            SearchInProgress = true;
            NotiflyStateChanged();
            TorrentsViewModel = await _httpClient.PostJsonAsync<TorrentsViewModel>(string.Format("api/Torrents/GetTorrents/?pageIndex={0}", pageIndex), criteria);
            SearchInProgress = false;
            NotiflyStateChanged();
        }

        private void NotiflyStateChanged() => OnChange?.Invoke();
    }
}
