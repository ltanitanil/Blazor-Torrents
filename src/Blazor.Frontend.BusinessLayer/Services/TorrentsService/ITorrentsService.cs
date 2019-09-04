using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blazor.Shared.ViewModels;
using Blazor.Shared.ViewModels.Search;
using Blazor.Shared.ViewModels.TorrentModel;

namespace Blazor.Frontend.BusinessLayer.Services.TorrentsService
{
    public interface ITorrentsService
    {
        Task<TorrentsViewModel> GetTorrentsAsync(SearchAndFilterCriteria criteria, int? pageIndex);
        Task<SearchAndFilterData> GetDataToFilter();
        Task<TorrentDescriptionView> GetTorrentDescription(int id);
    }
}
