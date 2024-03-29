﻿using System.Collections.Generic;
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
using Blazor.Shared.Core.Exceptions;

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

        public async Task<TorrentsViewModel> GetTorrentsAsync(SearchAndFilterCriteria criteria, int? pageIndex)
        {
            if (criteria == null)
                throw new AppException(ExceptionEvent.InvalidParameters, "Criteria can't be null.");
            if (pageIndex.HasValue && pageIndex.Value < 0)
                throw new AppException(ExceptionEvent.InvalidParameters, "Page can't be negative.");

            return await _customHttpClient.PostJsonAsync<TorrentsViewModel>($"api/Torrents/GetTorrents/?pageIndex={pageIndex}", criteria);
        }


        public async Task<SearchAndFilterData> GetDataToFilter() =>
            await _customHttpClient.GetJsonAsync<SearchAndFilterData>("api/Torrents/GetDataToFilter");

        public async Task<IReadOnlyList<CategoryView>> GetCategoriesWithSubcategories() =>
            await _customHttpClient.GetJsonAsync<IReadOnlyList<CategoryView>>("api/Torrents/GetCategoriesWithSubcategories");

        public async Task<TorrentDescriptionView> GetTorrentDescription(int id)
        {
            if (id < 0)
                throw new AppException(ExceptionEvent.InvalidParameters, "Torrent id can't be negative.");

            return await _customHttpClient.GetJsonAsync<TorrentDescriptionView>($"api/Torrents/GetTorrent/?id={id}");
        }


        public async Task UploadTorrent(TorrentUploadViewModel torrent, ElementReference filesRef)
        {
            if (torrent == null)
                throw new AppException(ExceptionEvent.InvalidParameters, "Torrent can't be null.");

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

        public async Task<string> GetLinkToDownloadFile(string directoryName, string fileName)
        {
            if(string.IsNullOrWhiteSpace(directoryName))
                throw new AppException(ExceptionEvent.InvalidParameters, "DirectoryName can't be null or empty.");
            if (string.IsNullOrWhiteSpace(directoryName))
                throw new AppException(ExceptionEvent.InvalidParameters, "FileName can't be null or empty.");

            return await _customHttpClient.GetJsonAsync<string>($"api/torrents/GetLinkToDownloadFile/{directoryName}/{fileName}");
        }
    }
}
