using System;
using Blazor.Server.BusinessLayer.Entities;

namespace Blazor.Server.BusinessLayer.Specifications
{
    public class CatalogFilterSpecification : BaseSpecification<Torrent>
    {
        public CatalogFilterSpecification(string search, int? forumId, long? sizeFrom, long? sizeTo, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
            : base(x => (string.IsNullOrEmpty(search) || x.Title.Contains(search))
                        && (!forumId.HasValue || x.ForumId == forumId)
                        && (!sizeFrom.HasValue || x.Size >= sizeFrom)
                        && (!sizeTo.HasValue || x.Size <= sizeTo)
                        && (!dateFrom.HasValue || x.RegisteredAt >= dateFrom)
                        && (!dateTo.HasValue || x.RegisteredAt <= dateTo))
        {
        }
    }
}
