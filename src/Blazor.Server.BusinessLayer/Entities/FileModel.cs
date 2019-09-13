using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Server.BusinessLayer.Entities
{
    public class FileModel
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public long Size { get; set; }

    }
}
