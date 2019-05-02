using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;
using Web.ViewModels.Torrent;

namespace Web.Interfaces
{
    public interface ITorrentsViewModelService
    {
        Task<TorrentsViewModel> GetTorrents(int pageIndex, int itemsPage, string selectedTitle);
        Task<TorrentDescriptionViewModel> GetTorrent(int id);
    }
}
