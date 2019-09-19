using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Shared.Models.ViewModels.TorrentModel
{
    public class TorrentDescriptionView : TorrentView
    {
        public string Content { get; set; }
        public string DirName { get; set; }
        public SubcategoryView Subcategory { get; set; }
        public IEnumerable<FileView> Files { get; set; }
    }
}
