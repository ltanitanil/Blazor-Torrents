using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Server.BusinessLayer.Entities
{
    public class Torrent : BaseEntity
    {
        public DateTimeOffset RegisteredAt { get; set; }
        public long Size { get; set; }
        public string Title { get; set; }
        public string Hash { get; set; }
        public int? TrackerId { get; set; }
        public string Content { get; set; }
        public string DirName { get; set; }

        public int ForumId { get; set; }
        public virtual Forum Forum { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public Torrent()
        {
            Files = new HashSet<File>();
        }
    }
}
