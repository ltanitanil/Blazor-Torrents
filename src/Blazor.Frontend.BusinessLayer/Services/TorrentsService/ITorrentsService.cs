using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Shared.Models.ViewModels;
using Blazor.Shared.Models.ViewModels.TorrentModel;
using Blazor.Shared.ViewModels;
using Blazor.Shared.ViewModels.Search;
using Microsoft.AspNetCore.Components;

namespace Blazor.Frontend.BusinessLayer.Services.TorrentsService
{
    public interface ITorrentsService
    {
        Task<TorrentsViewModel> GetTorrentsAsync(SearchAndFilterCriteria criteria, int? pageIndex);
        Task<SearchAndFilterData> GetDataToFilter();
        Task<IReadOnlyList<CategoryView>> GetCategoriesWithSubcategories();
        Task<TorrentDescriptionView> GetTorrentDescription(int id);
        Task UploadTorrent(TorrentUploadViewModel torrent, ElementReference filesRef);
        Task<string> GetLinkToDownloadFile(string directoryName, string fileName);
    }
}
