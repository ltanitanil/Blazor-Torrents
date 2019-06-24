using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blazor.Shared.ViewModels.Search
{
    public class SearchAndFilterCriteria
    {
        public int? SelectedForumId { get; set; }
        public string SearchText { get; set; }
        public Range<long?> Size { get; set; }
        public Range<DateTimeOffset?> Date { get; set; }

        public SearchAndFilterCriteria()
        {
            Size = new Range<long?>();
            Date = new Range<DateTimeOffset?>();
        }
    }

    public class Range<T>
    {
        public T From { get; set; }
        public T To { get; set; }
    }
}
