using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blazor.Server.BusinessLayer.Exceptions;
using Blazor.Server.BusinessLayer.Helpers;
using Blazor.Server.BusinessLayer.Services.BlobContainerService;
using Blazor.Server.DataAccessLayer.Repositories;
using Blazor.Server.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Blazor.Server.BusinessLayer.Services.TorrentsService
{
    public class TorrentsService : ITorrentsService
    {
        private readonly ITorrentsRepository _torrentsRepository;
        private readonly IBlobContainerService _blobContainer;

        public TorrentsService(ITorrentsRepository torrentsRepository, IBlobContainerService blobContainer)
        {
            _torrentsRepository = torrentsRepository;
            _blobContainer = blobContainer;
        }

        public async Task UploadTorrent(Torrent torrent, IEnumerable<IFormFile> files, string userName)
        {
            if (!files.Any())
                throw new AppException(ExceptionEvent.InvalidParameters, "List of files can't be empty");

            var fileLinksList = await _blobContainer.UploadFiles(files);

            torrent.Files = fileLinksList.Select(x =>
                new File { Name = x.Name, Link = x.Link, Size = x.Size }).ToList();
            torrent.UserName = userName;
            torrent.Size = fileLinksList.Sum(x => x.Size);

            await _torrentsRepository.AddAsync(torrent);
        }

        public async Task<(IReadOnlyList<Subcategory>, long)> GetDataToFilter(int forumsCount)
        {
            if (forumsCount < 0)
                throw new AppException(ExceptionEvent.InvalidParameters, "forumsCount can't be negative");

            return (await _torrentsRepository.GetPopularSubcategoriesAsync(forumsCount),
                await _torrentsRepository.MaxAsync(x => x.Size));
        }

        public async Task<Torrent> GetTorrent(int id)
        {
            if (id < 0)
                throw new AppException(ExceptionEvent.InvalidParameters, "Id can't be negative");

            var torrent = await _torrentsRepository.SingleAsync(expression: x => x.Id.Equals(id),
                              includeProperties: new List<Expression<Func<Torrent, object>>>
                              {
                                  x => x.Files,
                                  x => x.Subcategory,
                              }) ?? throw new AppException(ExceptionEvent.NotFound, $"Torrent(id={id}) not found");

            torrent.Content = BBCodeToHtmlConverter.Format(torrent.Content);

            return torrent;
        }

        public async Task<(IReadOnlyList<Torrent>, int)> GetTorrentsAndCount(int pageIndex, int itemsPerPageCount, string search,
            int? subcategoryId, long? sizeFrom, long? sizeTo, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
        {
            if (pageIndex < 0)
                throw new AppException(ExceptionEvent.InvalidParameters, "Page can't be negative");
            if (itemsPerPageCount < 0)
                throw new AppException(ExceptionEvent.InvalidParameters, "Number of elements per page can't be negative");

            Expression<Func<Torrent, bool>> filter = x => (string.IsNullOrEmpty(search) || x.Title.Contains(search))
                                                          && (!subcategoryId.HasValue || x.SubcategoryId == subcategoryId)
                                                          && (!sizeFrom.HasValue || x.Size >= sizeFrom)
                                                          && (!sizeTo.HasValue || x.Size <= sizeTo)
                                                          && (!dateFrom.HasValue || x.RegisteredAt >= dateFrom)
                                                          && (!dateTo.HasValue || x.RegisteredAt <= dateTo);

            var torrents = await _torrentsRepository.GetAll(filter)
                .Skip(pageIndex * itemsPerPageCount)
                .Take(itemsPerPageCount)
                .ToListAsync();

            if (!torrents.Any())
                throw new AppException(ExceptionEvent.NotFound, "Torrents not found");

            var count = await _torrentsRepository.GetAll(filter).CountAsync();

            return (torrents, count);
        }
    }
}
