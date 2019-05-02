using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class TorrentsViewModel
    {
        public IEnumerable<TorrentViewModel> CatalogTorrents { get; set; }
        public string SearchText { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}



