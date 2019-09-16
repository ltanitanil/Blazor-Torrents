using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blazor.Server.DataAccessLayer.Entities;
using Blazor.Server.DataAccessLayer.Repositories;

namespace Blazor.Server.DataAccessLayer.Context.Torrents
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Torrent> Torrents { get; }
        IRepository<File> Files { get; }
        IRepository<Category> Categories { get; }
        IRepository<Subcategory> Subcategories { get; }

    }
}
