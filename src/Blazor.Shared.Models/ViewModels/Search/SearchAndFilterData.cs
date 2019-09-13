using System.Collections.Generic;
using Blazor.Shared.Models.ViewModels.TorrentModel;

namespace Blazor.Shared.ViewModels.Search
{
    public class SearchAndFilterData
    {
        public IEnumerable<ForumView> Forums { get; set; }
        public long TorrentMaxSize { get; set; }
    }
}
