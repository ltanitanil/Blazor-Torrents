using System.Collections.Generic;
using Blazor.Shared.Models.ViewModels.TorrentModel;

namespace Blazor.Shared.ViewModels
{
    public class TorrentsViewModel
    {
        public IEnumerable<TorrentView> Torrents { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}
