using System;
using System.Collections.Generic;

namespace Blazor.Server.DataAccessLayer.Entities
{
    public class Torrent : BaseEntity
    {
        public DateTimeOffset RegisteredAt { get; set; }
        public string Title { get; set; }
        public long Size { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public string DirName { get; set; }
        public int SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }

        public ICollection<File> Files { get; set; }

        public Torrent()
        {
            Files = new HashSet<File>();
        }
    }
}
