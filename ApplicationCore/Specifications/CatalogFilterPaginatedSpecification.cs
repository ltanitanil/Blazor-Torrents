using ApplicationCore.Entities;

namespace ApplicationCore.Specifications
{
    public class CatalogFilterPaginatedSpecification : BaseSpecification<Torrent>
    {
        public CatalogFilterPaginatedSpecification(int skip,int take, string search)
            :base(x=>string.IsNullOrEmpty(search)||x.Title.Contains(search))
        {
            ApplyPaging(skip, take);
        }
    }
}
