using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class CatalogTorrentViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Size { get; set; } 
        public int CountFile { get; set; }
    }
}
