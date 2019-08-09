using System;

namespace Blazor.Core.Specifications
{
    public class CatalogFilterPaginatedSpecification : CatalogFilterSpecification
    {
        public CatalogFilterPaginatedSpecification(int skip, int take, string search, int? forumid, long? sizeFrom, long? sizeTo, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
            : base(search, forumid, sizeFrom, sizeTo, dateFrom, dateTo)
        {
            ApplyPaging(skip, take);
        }
    }
}
