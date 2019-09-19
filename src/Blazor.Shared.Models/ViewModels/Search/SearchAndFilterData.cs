using System.Collections.Generic;
using Blazor.Shared.Models.ViewModels.TorrentModel;

namespace Blazor.Shared.ViewModels.Search
{
    public class SearchAndFilterData
    {
        public IEnumerable<SubcategoryView> Subcategory { get; set; }
        public long TorrentMaxSize { get; set; }
    }
}
