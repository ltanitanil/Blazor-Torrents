using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Shared.ViewModels.Search
{
    public class SearchAndFilterCriteria
    {
        public int? SelectedForumId { get; set; }
        public string SearchText { get; set; }
        public Range<int?> Size { get; set; }
        public Range<DateTimeOffset?> Date { get; set; }
    }

    public class Range<T>
    {
        public T From { get; set; }
        public T To { get; set; }
    }
}
