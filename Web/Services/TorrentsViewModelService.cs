using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Helpers;
using Web.Interfaces;
using Web.ViewModels;
using Web.ViewModels.Torrent;

namespace Web.Services
{
    public class TorrentsViewModelService : ITorrentsViewModelService
    {
        private readonly IAsyncRepository<Torrent> _torrentRepository;

        public TorrentsViewModelService(IAsyncRepository<Torrent> torrentRepository)
        {
            _torrentRepository = torrentRepository;
        }

        public async Task<TorrentsViewModel> GetTorrents(int pageIndex, int itemsPage, string searchText)
        {
            var filterSpecification = new CatalogFilterSpecification(searchText);
            var filterPaginatedSpecification = new CatalogFilterPaginatedSpecification(itemsPage * pageIndex, itemsPage, searchText);

            var torrentsOnPage = await _torrentRepository.ListAsync(filterPaginatedSpecification);
            var totalTorrents = await _torrentRepository.CountAsync(filterSpecification);

            var ci = new TorrentsViewModel
            {
                CatalogTorrents = torrentsOnPage.Select(x => new TorrentViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Size = x.Size,
                    RegistredAt = x.RegistredAt
                }),
                SearchText = searchText,
                PaginationInfo = new PaginationInfoViewModel()
                {
                    ActualPage = pageIndex,
                    TorrentsPerPage = torrentsOnPage.Count,
                    TotalTorrents = totalTorrents,
                    TotalPages = int.Parse(Math.Ceiling((decimal)totalTorrents / itemsPage).ToString())
                }
            };
            ci.PaginationInfo.Previous = (ci.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";
            ci.PaginationInfo.Next = (ci.PaginationInfo.ActualPage == ci.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            return ci;
        }

        public async Task<TorrentDescriptionViewModel> GetTorrent(int id)
        {
            var torrent = await _torrentRepository.GetByIdAsync(id);
            var t = new TorrentDescriptionViewModel()
            {
                Title = torrent.Title,
                RegistredAt = torrent.RegistredAt,
                Size = torrent.Size,
                Content = BBCodeHelper.Format(torrent.Content),
                Dir_Name = torrent.Dir_Name,
                Forum = torrent.Forum,
                File = torrent.Files
            };
            return t;
        }
    }
}
