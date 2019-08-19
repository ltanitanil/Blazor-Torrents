using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blazor.Shared.ViewModels
{
    public class PaginationInfoViewModel
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
