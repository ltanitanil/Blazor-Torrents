using Blazor.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Core.Specifications
{
    public class CatalogFilterSpecification : BaseSpecification<Torrent>
    {
        public CatalogFilterSpecification(string search, int? forumid, long? sizeFrom, long? sizeTo, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
            : base(x => (string.IsNullOrEmpty(search) || x.Title.Contains(search))
                        && (!forumid.HasValue || x.ForumId == forumid)
                        && (!sizeFrom.HasValue || x.Size >= sizeFrom.Value)
                        && (!sizeTo.HasValue || x.Size <= sizeTo.Value)
                        && ((!dateFrom.HasValue && !dateTo.HasValue) || (dateFrom <= x.RegisteredAt && x.RegisteredAt <= dateTo)))
        {
        }
    }
}
