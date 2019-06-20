using Blazor.Shared.ViewModels.TorrentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Shared.ViewModels
{
    public class TorrentsViewModel
    {
        public IEnumerable<TorrentView> Torrents { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}
