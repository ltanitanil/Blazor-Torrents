using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Shared.Models.ViewModels.TorrentModel
{
    public class CategoryView
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public IEnumerable<SubcategoryView> Subcategories { get; set; }

        public CategoryView()
        {
            Subcategories= new HashSet<SubcategoryView>();
        }
    }
}
