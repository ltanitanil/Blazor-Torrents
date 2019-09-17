using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Server.DataAccessLayer.Entities
{
    public class Category : BaseEntity
    {
        public string Title { get; set; }

        public IEnumerable<Subcategory> Subcategories { get; set; }

        public Category()
        {
            Subcategories = new HashSet<Subcategory>();
        }
    }
}
