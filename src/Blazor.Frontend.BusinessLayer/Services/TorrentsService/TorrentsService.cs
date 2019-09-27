using System.Collections.Generic;
using Blazor.Shared.ViewModels;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Blazor.Shared.ViewModels.Search;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using Blazor.FileReader;
using Blazor.Shared.Models.ViewModels;
using Blazor.Shared.Models.ViewModels.TorrentModel;

namespace Blazor.Frontend.BusinessLayer.Services.TorrentsService
{
    public class TorrentsService : ITorrentsService
    {
        private readonly HttpClient _httpClient;
        private readonly IFileReaderService _fileReaderService;

        public TorrentsService(HttpClient httpClient, IFileReaderService fileReaderService)
        {
            _httpClient = httpClient;
            _fileReaderService = fileReaderService;
        }

        public async Task<TorrentsViewModel> GetTorrentsAsync(SearchAndFilterCriteria criteria, int? pageIndex) =>
            await _httpClient.PostJsonAsync<TorrentsViewModel>($"api/Torrents/GetTorrents/?pageIndex={pageIndex}", criteria);

        public async Task<SearchAndFilterData> GetDataToFilter() =>
            await _httpClient.GetJsonAsync<SearchAndFilterData>("api/Torrents/GetDataToFilter");

        public async Task<IReadOnlyList<CategoryView>> GetCategoriesWithSubcategories() =>
            await _httpClient.GetJsonAsync<IReadOnlyList<CategoryView>>("api/Torrents/GetCategoriesWithSubcategories");

        public async Task<TorrentDescriptionView> GetTorrentDescription(int id) =>
            await _httpClient.GetJsonAsync<TorrentDescriptionView>($"api/Torrents/GetTorrent/?id={id}");

        public async Task<ResponseResult> UploadTorrent(TorrentUploadViewModel torrent, ElementReference filesRef)
        {
            using var content = new MultipartFormDataContent
            {
                { new StringContent(JsonSerializer.Serialize(torrent), Encoding.UTF8, "application/json"), "json" }
            };

            foreach (var file in await _fileReaderService.CreateReference(filesRef).EnumerateFilesAsync())
            {
                var fileInfo = await file.ReadFileInfoAsync();
                content.Add(new StreamContent(await file.OpenReadAsync()), "files", fileInfo.Name);
            }

            using var response = await _httpClient.PostAsync("api/torrents/UploadTorrent", content);

            return new ResponseResult
            {
                IsSuccessful = response.IsSuccessStatusCode,
                ContentResult = await response.Content.ReadAsStringAsync()
            }; 
        }

        public async Task<string> GetLinkToDownloadFile(string directoryName, string fileName) => 
            await _httpClient.GetStringAsync($"api/torrents/GetLinkToDownloadFile/{directoryName}/{fileName}");
    }
}
