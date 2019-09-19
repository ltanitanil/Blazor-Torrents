using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blazor.Server.DataAccessLayer.Context;
using Blazor.Server.DataAccessLayer.Entities;
using Blazor.Server.DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.DataAccessLayer.Context.Torrents
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TorrentsContext _context;
        private bool _disposed;

        public UnitOfWork(TorrentsContext context,
            IRepository<Torrent> torrents,
            IRepository<File> files,
            IRepository<Category> categories,
            IRepository<Subcategory> subcategories)
        {
            _context = context;
            Torrents = torrents;
            Files = files;
            Categories = categories;
            Subcategories = subcategories;
        }

        public IRepository<Torrent> Torrents { get; }
        public IRepository<File> Files { get; }
        public IRepository<Category> Categories { get; }
        public IRepository<Subcategory> Subcategories { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)return;
            if(disposing) _context.Dispose();

            _disposed = true;
        }
    }
}
