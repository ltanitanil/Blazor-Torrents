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
        public void CatalogFilterPaginatedSpecification_SpecificationParameters_ReturnValidSpecification(int skip, 
            int take, string search, int? forumId, long? sizeFrom, long? sizeTo, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
        {
            // Act
            var specification = new CatalogFilterPaginatedSpecification(skip, take, search, forumId, sizeFrom, sizeTo, dateFrom, dateTo);

            // Assert
            Assert.NotNull(specification.Criteria);
            Assert.True(specification.IsPagingEnabled);
            Assert.True(specification.Skip == skip,
                $"Expected skip={skip} doesn't match the actual skip={specification.Skip}");
            Assert.True(specification.Take == take,
                $"Expected take={take} doesn't match the actual take={specification.Take}");
        }
    }
}
