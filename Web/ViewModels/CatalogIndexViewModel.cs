using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class CatalogIndexViewModel
    {
        public IEnumerable<CatalogTorrentViewModel> CatalogTorrents { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}
