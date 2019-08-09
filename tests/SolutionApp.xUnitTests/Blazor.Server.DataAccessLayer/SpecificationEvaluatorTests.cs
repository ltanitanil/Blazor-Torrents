using System;
using System.Linq;
using Blazor.Server.BusinessLayer.Entities;
using Blazor.Server.BusinessLayer.Interfaces;
using Blazor.Server.BusinessLayer.Specifications;
using Blazor.Server.DataAccessLayer.Data;
using Tests.Shared;
using Xunit;

namespace SolutionApp.xUnitTests.Blazor.Server.DataAccessLayer
{
    public class SpecificationEvaluatorTests
    {
        public static TheoryData<ISpecification<Torrent>, int> Data =>
            new TheoryData<ISpecification<Torrent>, int> {
                { new CatalogFilterPaginatedSpecification(0,5,null,null,null,null,null,null), 5},
                { new CatalogFilterPaginatedSpecification(1,5,"Torrent",1,long.MinValue,long.MaxValue,DateTimeOffset.MinValue, DateTimeOffset.MaxValue), 1},
                { new CatalogFilterSpecification("Torrent",1,long.MinValue,long.MaxValue,DateTimeOffset.MinValue, DateTimeOffset.MaxValue),2},
                { null, 11 }};

        [Theory]
        [MemberData(nameof(Data))]
        public void GetQuery_SpecificationAndExpectedCount_ReturnExpectedNumberOfTorrents(ISpecification<Torrent> specification, int expectedCount)
        {
            // Arrange
            var items = InitialEntities.Torrents;

            // Act
            var torrents = SpecificationEvaluator<Torrent>.GetQuery(items.AsQueryable(), specification);

            // Assert
            Assert.NotNull(torrents);
            Assert.True(torrents.Count() == expectedCount, 
                $"Current count={torrents.Count()} doesn't match expected count={expectedCount}");
        }

        [Fact]
        public void GetQuery_InputQueryIsNull_ReturnException() =>
            Assert.Throws<ArgumentNullException>(() =>
                SpecificationEvaluator<Torrent>.GetQuery(inputQuery: null, new CatalogFilterPaginatedSpecification(0, 5, null, null, null, null, null, null)));
    }
}
