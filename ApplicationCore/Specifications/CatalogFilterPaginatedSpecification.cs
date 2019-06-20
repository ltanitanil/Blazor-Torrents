using ApplicationCore.Entities;
using System;

namespace ApplicationCore.Specifications
{
    public class CatalogFilterPaginatedSpecification : BaseSpecification<Torrent>
    {
        public CatalogFilterPaginatedSpecification(int skip,int take, string search, int? forumid, int? sizeFrom, int? sizeTo, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
            : base(x => (string.IsNullOrEmpty(search) || x.Title.Contains(search))
                        && (!forumid.HasValue || x.ForumId == forumid))
                        //добавить поиск по дате и размеру
        {
            ApplyPaging(skip, take);
        }
    }
}
