using System;
using System.Collections.Generic;
using System.Text;
using Blazor.Core.Specifications;
using Xunit;

namespace SolutionApp.xUnitTests.Core
{
    public class CatalogFilterSpecificationTests
    {
        public static TheoryData<string, int?, long?, long?, DateTimeOffset?, DateTimeOffset?> Data =>
            new TheoryData<string, int?, long?, long?, DateTimeOffset?, DateTimeOffset?>
            {
                { null, null, null, null, null, null},
                { string.Empty, 5, long.MinValue, long.MaxValue, DateTimeOffset.MinValue, DateTimeOffset.MaxValue}
            };

        [Theory]
        [MemberData(nameof(Data))]
        public void ReturnValidCatalogFilterPaginatedSpecification(string search, int? forumId, long? sizeFrom, long? sizeTo, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
        {
            var result = new CatalogFilterSpecification(search, forumId, sizeFrom, sizeTo, dateFrom, dateTo);

            Assert.NotNull(result.Criteria);
            Assert.False(result.IsPagingEnabled);
        }
    }
}
