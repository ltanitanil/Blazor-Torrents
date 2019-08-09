using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blazor.Core.Entities;
using Blazor.Core.Interfaces;
using Blazor.Core.Specifications;
using Blazor.Infrastructure.Data;
using Blazor.Infrastructure.Data.Repositories;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Tests.Shared;
using Xunit;

namespace SolutionApp.xUnitTests.Infrastructure.Repositories
{
    public class TorrentsRepositoryTests
    {
        private readonly ITorrentsRepository _torrentsRepository;

        public TorrentsRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<CatalogContext>().Options;
            var catalogContextMock = new DbContextMock<CatalogContext>(options);

            catalogContextMock.CreateDbSetMock(x => x.Forums, InitialEntities.Forums);
            catalogContextMock.CreateDbSetMock(x => x.Torrents, InitialEntities.Torrents);
            catalogContextMock.CreateDbSetMock(x => x.Files, InitialEntities.Files);

            _torrentsRepository = new TorrentsRepository(catalogContextMock.Object);
        }

        #region GetByIdAsync()_Tests

        [Fact]
        public async Task GetByIdAsync_5_ReturnExistingTorrent()
        {
            // Arrange
            const int id = 5;

            // Act
            var result = await _torrentsRepository.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.True(id == result.Id, $"Current id={result.Id} doesn't match expected id={id}");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(55)]
        public async Task GetByIdAsync_NonExistingId_ReturnNull(int id)
        {
            // Act
            var result = await _torrentsRepository.GetByIdAsync(id);

            // Assert
            Assert.Null(result);
        }

        #endregion GetByIdAsync_Tests 

        #region ListAsync()_Tests

        [Fact]
        public async Task ListAsync_SpecificationWithTheConditionToTake3_ReturnTorrentsAccordingToTheSpecification()
        {
            // Arrange
            const int expectedCount = 3;
            var specification =
                new CatalogFilterPaginatedSpecification(0, expectedCount, null, null, null, null, null, null);

            // Act
            var result = await _torrentsRepository.ListAsync(specification);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count == expectedCount, 
                $"Current count={result.Count} doesn't match expected count={expectedCount}");
        }

        [Theory]
        [InlineData(null)]
        public async Task ListAsync_SpecificationIsNull_ReturnAllTorrents(ISpecification<Torrent> specification)
        {
            // Arrange
            const int expectedCount = 11;

            // Act
            var result = await _torrentsRepository.ListAsync(specification);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count == expectedCount, 
                $"Current count={result.Count} doesn't match expected count={expectedCount}");
        }

        #endregion

        #region CountAsync()_Tests

        [Theory]
        [InlineData(null)]
        public async Task CountAsync_SpecificationIsNull_ReturnActualCount(ISpecification<Torrent> specification)
        {
            // Arrange
            const int expectedCount = 11;

            // Act
            var result = await _torrentsRepository.CountAsync(specification);

            // Assert
            Assert.True(result == expectedCount, 
                $"Current count={result} doesn't match expected count={expectedCount}");
        }

        [Fact]
        public async Task CountAsync_SpecificationWithTheConditionToTake2_ReturnActualCount()
        {
            // Arrange
            const int expectedCount = 2;
            var spec =
                new CatalogFilterPaginatedSpecification(0, expectedCount, null, null, null, null, null, null);

            // Act
            var result = await _torrentsRepository.CountAsync(spec);

            // Assert
            Assert.True(result == expectedCount, $"Current count={result} doesn't match expected count={expectedCount}");
        }

        #endregion

        #region GetPopularForumsAsync()_Tests

        [Fact]
        public async Task GetPopularForumsAsync_3_ReturnTheCorrectNumberOfPopularForums()
        {
            // Arrange
            const int expectedCount = 3;

            // Act
            var result = await _torrentsRepository.GetPopularForumsAsync(expectedCount);

            // Assert
            Assert.NotNull(result);
            Assert.True(expectedCount == result.Count, 
                $"Current count={result.Count} doesn't match expected count={expectedCount}");
        }

        #endregion GetPopularForumsAsync

        #region GetMaxTorrentSizeAsync()_Tests

        [Fact]
        public async Task GetMaxTorrentSizeAsync_999999999_ReturnActualMaximumTorrentSize()
        {
            // Arrange
            const long maxSize = 999999999;

            // Act
            var result = await _torrentsRepository.GetMaxTorrentSizeAsync();

            // Assert
            Assert.Equal(maxSize, result);
        }

        #endregion
    }
}
