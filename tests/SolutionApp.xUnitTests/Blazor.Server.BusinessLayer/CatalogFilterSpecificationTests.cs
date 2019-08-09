using System;
using Blazor.Server.BusinessLayer.Specifications;
using Xunit;

namespace SolutionApp.xUnitTests.Blazor.Server.BusinessLayer
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
        public void CatalogFilterSpecification_SpecificationParameters_ReturnValidSpecification(string search, 
            int? forumId, long? sizeFrom, long? sizeTo, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
        {
            // Act
            var specification = new CatalogFilterSpecification(search, forumId, sizeFrom, sizeTo, dateFrom, dateTo);

            // Assert
            Assert.NotNull(specification.Criteria);
            Assert.False(specification.IsPagingEnabled);
        }
    }
}
