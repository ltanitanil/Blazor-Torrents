using Blazor.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Infrastructure.Data
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
