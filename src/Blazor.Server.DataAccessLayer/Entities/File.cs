﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Server.DataAccessLayer.Entities
{
    public class File : BaseEntity
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public string Link { get; set; }

        public int TorrentId { get; set; }

    }
}
