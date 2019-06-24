using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Specifications
{
    public class CatalogFilterSpecification : BaseSpecification<Torrent>
    {
        public CatalogFilterSpecification(string search, int? forumid, long? sizeFrom, long? sizeTo, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
            : base(x => (string.IsNullOrEmpty(search) || x.Title.Contains(search))
                        && (!forumid.HasValue || x.ForumId == forumid)
                        && ((!sizeFrom.HasValue&&!sizeTo.HasValue)||(sizeFrom<=x.Size&&x.Size<=sizeTo))
                        &&((!dateFrom.HasValue&&!dateTo.HasValue)||(dateFrom<=x.RegistredAt&&x.RegistredAt<=dateTo)))
        {
        }
    }
}
