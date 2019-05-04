using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels.Torrent
{
    public class TorrentDescriptionViewModel:TorrentViewModel
    {
        public string Content { get; set; }
        public string Dir_Name { get; set; }
        public Forum Forum { get; set; }
        public IEnumerable<File> Files { get; set; }
    }
}
