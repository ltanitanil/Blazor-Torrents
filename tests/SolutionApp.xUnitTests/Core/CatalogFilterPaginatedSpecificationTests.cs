using Blazor.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SolutionApp.xUnitTests.Core
{
    public class CatalogFilterPaginatedSpecificationTests
    {
        public static TheoryData<int, int, string, int?, long?, long?, DateTimeOffset?, DateTimeOffset?> Data =>
            new TheoryData<int, int, string, int?, long?, long?, DateTimeOffset?, DateTimeOffset?>
            {
                { 0, 0, null, null, null, null, null, null},
                { 5, 5, string.Empty, 5, long.MinValue, long.MaxValue, DateTimeOffset.MinValue, DateTimeOffset.MaxValue}
            };

        [Theory]
        [MemberData(nameof(Data))]
        public void ReturnValidCatalogFilterPaginatedSpecification(int skip, int take, string search, int? forumId, long? sizeFrom, long? sizeTo, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
        {
            var result = new CatalogFilterPaginatedSpecification(skip, take, search, forumId, sizeFrom, sizeTo, dateFrom, dateTo);

            Assert.NotNull(result.Criteria);
            Assert.True(result.IsPagingEnabled);
            Assert.True(result.Skip == skip, $"Expected skip={skip} doesn't match the actual skip={result.Skip}");
            Assert.True(result.Take == take, $"Expected take={take} doesn't match the actual take={result.Take}");
        }
    }
}
