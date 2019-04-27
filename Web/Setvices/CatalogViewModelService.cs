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
        public CatalogIndexViewModel GetCatalogItems(int pageIndex, int itemsPage)
        {
            _torrentRepository.ListAsync();
            CatalogTorrentViewModel torrentViewModel = new CatalogTorrentViewModel()
            {
                Id = 1,
                CountFile = 8,
                Size = "215214",
                Title = "hi!hi!hi!hi!hi!HI!hI!"
            };
            return new CatalogIndexViewModel { catalogTorrents = new CatalogTorrentViewModel[] {torrentViewModel}};
        }
    }
}
