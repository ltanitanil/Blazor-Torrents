using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        public async Task GetByIdAsync_Return_Existing_Torrent_By_Id()
        {
            const int id = 5;

            var result = await _torrentsRepository.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.True(id == result.Id, $"Current id={result.Id} doesn't match expected id={id}");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(55)]
        public async Task GetByIdAsync_Return_Null_If_Torrent_Does_Not_Exist(int id)
        {
            var result = await _torrentsRepository.GetByIdAsync(id);

            Assert.Null(result);
        }

        #endregion GetByIdAsync_Tests 

        #region ListAsync()_Tests

        [Fact]
        public async Task ListAsync_ReturnExistingTorrents()
        {
            const int expectedCount = 3;
            var specification =
                new CatalogFilterPaginatedSpecification(0, expectedCount, null, null, null, null, null, null);

            var result = await _torrentsRepository.ListAsync(specification);

            Assert.NotNull(result);
            Assert.True(result.Count == expectedCount, $"Current count={result.Count} doesn't match expected count={expectedCount}");
        }

        [Fact]
        public async Task ListAsync_Return_All_Torrents_If_Specification_Is_Null()
        {
            const int expectedCount = 5;

            var result = await _torrentsRepository.ListAsync(null);

            Assert.NotNull(result);
            Assert.True(result.Count == expectedCount, $"Current count={result.Count} doesn't match expected count={expectedCount}");
        }

        #endregion

        #region CountAsync()_Tests

        [Fact]
        public async Task CountAsync_Return_Actual_Count_Without_Specification()
        {
            const int expectedCount = 5;

            var result = await _torrentsRepository.CountAsync(null);

            Assert.True(result == expectedCount, $"Current count={result} doesn't match expected count={expectedCount}");
        }

        [Fact]
        public async Task CountAsync_Return_Actual_Count_With_Specification()
        {
            const int expectedCount = 2;
            var spec =
                new CatalogFilterPaginatedSpecification(0, expectedCount, null, null, null, null, null, null);

            var result = await _torrentsRepository.CountAsync(spec);

            Assert.True(result == expectedCount, $"Current count={result} doesn't match expected count={expectedCount}");
        }

        #endregion

        #region GetPopularForumsAsync()_Tests

        [Fact]
        public async Task GetPopularForumsAsync_Return_Popular_Forums_in_the_Requested_Quantity()
        {
            const int expectedCount = 3;

            var result = await _torrentsRepository.GetPopularForumsAsync(expectedCount);

            Assert.NotNull(result);
            Assert.True(expectedCount == result.Count, $"Current count={result.Count} doesn't match expected count={expectedCount}");
        }

        #endregion GetPopularForumsAsync

        #region GetMaxTorrentSizeAsync()_Tests

        [Fact]
        public async Task GetMaxTorrentSizeAsync_Return_Valid_Maximum_Torrent_Size()
        {
            const long maxSize = 55555;

            var result = await _torrentsRepository.GetMaxTorrentSizeAsync();

            Assert.Equal(maxSize, result);
        }

        #endregion
    }
}
