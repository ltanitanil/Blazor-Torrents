using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class File : BaseEntity
    {
        public string Name { get; set; }
        public string Size { get; set; }

        //public int TorrentId { get; set; }

    }
}
