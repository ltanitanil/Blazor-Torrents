using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Setvices
{
    public class CatalogViewModelService : ICatalogViewModelService
    {
        private readonly IAsyncRepository<Torrent> _torrentRepository;
        private readonly IAsyncRepository<File> _fileRepository;
        private readonly IAsyncRepository<Forum> _forumRepository;

        public CatalogViewModelService(
            IAsyncRepository<Torrent> torrentRepository,
            IAsyncRepository<File> fileRepository,
            IAsyncRepository<Forum> forumRepository)
        {
            _torrentRepository = torrentRepository;
            _fileRepository = fileRepository;
            _forumRepository = forumRepository;
        }

        //тут должен быть таск
        public async Task<CatalogIndexViewModel> GetCatalogItems(int pageIndex, int itemsPage)
        {
            var torrentsOnPage = await _torrentRepository.ListAsync();
            var ci = new CatalogIndexViewModel
            {
                CatalogTorrents = torrentsOnPage.Select(x => new CatalogTorrentViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Size = x.Size,
                }),
                PaginationInfo = new PaginationInfoViewModel()
                {
                    ActualPage= pageIndex,
                    TorrentsPerPage = torrentsOnPage.Count,
                    TotalTorrents = 50,//correct
                    TotalPages = 0//correct
                }
            };



            return ci;
        }
    }
}
