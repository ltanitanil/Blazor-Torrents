using System;

namespace Blazor.Server.BusinessLayer.Specifications
{
    public class CatalogFilterPaginatedSpecification : CatalogFilterSpecification
    {
        public CatalogFilterPaginatedSpecification(int skip, int take, string search, int? forumId, long? sizeFrom, long? sizeTo, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
            : base(search, forumId, sizeFrom, sizeTo, dateFrom, dateTo)
        {
            ApplyPaging(skip, take);
        }
    }
}
