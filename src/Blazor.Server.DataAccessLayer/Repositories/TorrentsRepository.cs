using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Blazor.Server.DataAccessLayer.Context;
using Blazor.Server.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.DataAccessLayer.Repositories
{
    public class TorrentsRepository : Repository<Torrent>, ITorrentsRepository
    {
        public TorrentsRepository(TorrentsContext context) : base(context) { }

        public async Task<IReadOnlyList<Subcategory>> GetPopularSubcategoriesAsync(int count)
        {
            return await _dbSet
                .GroupBy(x => x.SubcategoryId, (key, items) => new { Key = key, Count = items.Count() })
                .OrderByDescending(x => x.Count)
                .Take(count)
                .Join(_context.Subcategories, (t) => t.Key, (f) => f.Id, (t, f) => f)
                .ToListAsync();
        }

    }
}
