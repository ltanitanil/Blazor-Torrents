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
using Blazor.Frontend.BusinessLayer.Services.CustomHTTPClient;


namespace Blazor.Frontend.BusinessLayer.Services.TorrentsService
{
    public class TorrentsService : ITorrentsService
    {
        private readonly CustomHttpClient _customHttpClient;
        private readonly IFileReaderService _fileReaderService;

        public TorrentsService(CustomHttpClient customHttpClient, IFileReaderService fileReaderService)
        {
            _customHttpClient = customHttpClient;
            _fileReaderService = fileReaderService;
        }

        public async Task<TorrentsViewModel> GetTorrentsAsync(SearchAndFilterCriteria criteria, int? pageIndex) =>
            await _customHttpClient.PostJsonAsync<TorrentsViewModel>($"api/Torrents/GetTorrents/?pageIndex={pageIndex}", criteria);

        public async Task<SearchAndFilterData> GetDataToFilter() =>
            await _customHttpClient.GetJsonAsync<SearchAndFilterData>("api/Torrents/GetDataToFilter");

        public async Task<IReadOnlyList<CategoryView>> GetCategoriesWithSubcategories() =>
            await _customHttpClient.GetJsonAsync<IReadOnlyList<CategoryView>>("api/Torrents/GetCategoriesWithSubcategories");

        public async Task<TorrentDescriptionView> GetTorrentDescription(int id) =>
            await _customHttpClient.GetJsonAsync<TorrentDescriptionView>($"api/Torrents/GetTorrent/?id={id}");

        public async Task UploadTorrent(TorrentUploadViewModel torrent, ElementReference filesRef)
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

            await _customHttpClient.SendAsync(HttpMethod.Post, "api/torrents/UploadTorrent", content);
        }

        public async Task<string> GetLinkToDownloadFile(string directoryName, string fileName) =>
            await _customHttpClient.GetJsonAsync<string>($"api/torrents/GetLinkToDownloadFile/{directoryName}/{fileName}");
    }
}
