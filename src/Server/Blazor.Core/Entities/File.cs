using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.BusinessLayer.Entities
{
    public class File : BaseEntity
    {
        public string Name { get; set; }
        public long Size { get; set; }

        public int TorrentId { get; set; }

    }
}
