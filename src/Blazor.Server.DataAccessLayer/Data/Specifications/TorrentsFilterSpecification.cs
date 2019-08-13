using System;
using Blazor.Server.DataAccessLayer.Data.Entities;

namespace Blazor.Server.DataAccessLayer.Data.Specifications
{
    public class TorrentsFilterSpecification : BaseSpecification<Torrent>
    {
        public TorrentsFilterSpecification(string search, int? forumId, long? sizeFrom, long? sizeTo, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
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
