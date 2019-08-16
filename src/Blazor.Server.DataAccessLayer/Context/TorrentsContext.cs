using Microsoft.EntityFrameworkCore;
using Blazor.Server.DataAccessLayer.Entities;

namespace Blazor.Server.DataAccessLayer.Context
{
    public class TorrentsContext : DbContext
    {
        public virtual DbSet<Torrent> Torrents { get; set; }
        public virtual DbSet<Forum> Forums { get; set; }
        public virtual DbSet<File> Files { get; set; }

        public TorrentsContext(DbContextOptions<TorrentsContext> options) : base(options)
        {
        }

    }
}
