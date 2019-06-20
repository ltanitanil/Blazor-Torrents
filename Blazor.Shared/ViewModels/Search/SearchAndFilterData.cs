using Blazor.Shared.ViewModels.TorrentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Shared.ViewModels.Search
{
    public class SearchAndFilterData
    {
        public IEnumerable<ForumView> Forums { get; set; }
        public int TorrentMaxSize { get; set; }
    }
}
