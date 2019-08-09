using Blazor.Shared.ViewModels;
using Blazor.Shared.ViewModels.Search;
using Blazor.Shared.ViewModels.TorrentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Server.Interfaces
{
    public interface ITorrentsViewModelService
    {
        Task<TorrentsViewModel> GetTorrents(int pageIndex, int itemsPage, SearchAndFilterCriteria criteria);
        Task<TorrentDescriptionView> GetTorrent(int id);
        Task<SearchAndFilterData> GetDataToFilter(int forumsCount);
    }
}
