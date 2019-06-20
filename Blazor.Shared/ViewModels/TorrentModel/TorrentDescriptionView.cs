using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Shared.ViewModels.TorrentModel
{
    public class TorrentDescriptionView:TorrentView
    {
        public string Content { get; set; }
        public string Dir_Name { get; set; }
        public ForumView Forum { get; set; }
        public IEnumerable<FileView> Files { get; set; }
    }
}
