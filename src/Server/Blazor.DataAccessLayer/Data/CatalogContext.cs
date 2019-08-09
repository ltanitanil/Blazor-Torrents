using Microsoft.EntityFrameworkCore;
using Blazor.BusinessLayer.Entities;

namespace Blazor.DataAccessLayer.Data
{
    public class CatalogContext : DbContext
    {
        public virtual DbSet<Torrent> Torrents { get; set; }
        public virtual DbSet<Forum> Forums { get; set; }
        public virtual DbSet<File> Files { get; set; }

        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }

    }
}
