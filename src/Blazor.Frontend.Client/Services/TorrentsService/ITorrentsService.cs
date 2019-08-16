using Blazor.Shared.ViewModels;
using Blazor.Shared.ViewModels.Search;
using Blazor.Shared.ViewModels.TorrentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Frontend.Client.Services.TorrentsService
{
    public interface ITorrentsService
    {
        Task<TorrentsViewModel> GetTorrentsAsync(SearchAndFilterCriteria criteria, int? pageIndex);
        Task<SearchAndFilterData> GetDataToFilter();
        Task<TorrentDescriptionView> GetTorrentDescription(int id);
    }
}
