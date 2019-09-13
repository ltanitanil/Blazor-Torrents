using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blazor.Server.DataAccessLayer.Entities;

namespace Blazor.Server.DataAccessLayer.Repositories
{
    public interface ITorrentsRepository : IRepository<Torrent>
    {
        Task<IReadOnlyList<Subcategory>> GetPopularSubcategoriesAsync(int count);
    }
}
