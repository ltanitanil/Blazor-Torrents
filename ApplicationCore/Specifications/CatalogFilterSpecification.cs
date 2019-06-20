using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Specifications
{
    public class CatalogFilterSpecification : BaseSpecification<Torrent>
    {
        public CatalogFilterSpecification(string search, int? forumid, int? sizeFrom, int? sizeTo, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
            : base(x => (string.IsNullOrEmpty(search) || x.Title.Contains(search))
                        && (!forumid.HasValue || x.ForumId == forumid))
                        //добавить поиск по дате и размеру
        {
        }
    }
}
