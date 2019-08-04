using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blazor.Core.Entities;
using Blazor.Core.Interfaces;
using Blazor.Core.Specifications;
using Blazor.Infrastructure.Data;
using Tests.Shared;
using Xunit;

namespace SolutionApp.xUnitTests.Infrastructure
{
    public class SpecificationEvaluatorTests
    {
        public static TheoryData<ISpecification<Torrent>, int> Data =>
            new TheoryData<ISpecification<Torrent>, int> {
                { new CatalogFilterPaginatedSpecification(0,5,null,null,null,null,null,null), 5},
                { new CatalogFilterPaginatedSpecification(1,5,"Torrent",1,long.MinValue,long.MaxValue,DateTimeOffset.MinValue, DateTimeOffset.MaxValue), 1},
                { new CatalogFilterSpecification("Torrent",1,long.MinValue,long.MaxValue,DateTimeOffset.MinValue, DateTimeOffset.MaxValue),2},
                { null, 5 }};

        [Theory]
        [MemberData(nameof(Data))]
        public void ReturnExpectedNumberOfTorrents(ISpecification<Torrent> specification, int expectedCount)
        {
            var items = InitialEntities.Torrents;

            var result = SpecificationEvaluator<Torrent>.GetQuery(items.AsQueryable(), specification);

            Assert.NotNull(result);
            Assert.True(result.Count() == expectedCount, $"Current count={result.Count()} doesn't match expected count={expectedCount}");
        }

        [Fact]
        public void ReturnExceptionIfInputQueryIsNull() =>
            Assert.Throws<ArgumentNullException>(() =>
                SpecificationEvaluator<Torrent>.GetQuery(inputQuery: null, new CatalogFilterPaginatedSpecification(0, 5, null, null, null, null, null, null)));
    }
}
