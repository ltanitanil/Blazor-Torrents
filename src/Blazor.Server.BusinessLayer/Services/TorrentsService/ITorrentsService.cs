using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blazor.Server.BusinessLayer.Entities;
using Blazor.Server.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;

namespace Blazor.Server.BusinessLayer.Services.TorrentsService
{
    public interface ITorrentsService
    {
        Task<(IReadOnlyList<Torrent>,int)> GetTorrentsAndCount(int pageIndex, int itemsPage, string search, int? subcategoryId, long? sizeFrom, long? sizeTo, DateTimeOffset? dateFrom, DateTimeOffset? dateTo);
        Task<IReadOnlyList<Category>> GetCategoriesWithSubcategories();
        Task<Torrent> GetTorrent(int id);
        Task<(IReadOnlyList<Subcategory>, long)> GetDataToFilter(int forumsCount);
        Task UploadTorrent(Torrent torrent,IEnumerable<IFormFile> files,string userName);
    }
}
