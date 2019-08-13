using System;

namespace Blazor.Server.DataAccessLayer.Data.Specifications
{
    public class TorrentsFilterPaginatedSpecification : TorrentsFilterSpecification
    {
        public TorrentsFilterPaginatedSpecification(int skip, int take, string search, int? forumId, long? sizeFrom, long? sizeTo, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
            : base(search, forumId, sizeFrom, sizeTo, dateFrom, dateTo)
        {
            ApplyPaging(skip, take);
        }
    }
}
