using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Specifications
{
    public class CatalogFilterSpecification: BaseSpecification<Torrent>
    {
        public CatalogFilterSpecification(string search)
            :base(x=>string.IsNullOrEmpty(search)||x.Title.Contains(search))
        {
        }
    }
}
