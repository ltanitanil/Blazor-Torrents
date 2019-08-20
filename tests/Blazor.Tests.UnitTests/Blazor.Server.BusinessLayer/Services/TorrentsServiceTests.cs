using System;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Server.BusinessLayer.Exceptions;
using Blazor.Server.BusinessLayer.Services.TorrentsService;
using Blazor.Server.DataAccessLayer.Context;
using Blazor.Server.DataAccessLayer.Repositories;
using Blazor.Tests.Helpers;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Blazor.Tests.UnitTests.Blazor.Server.BusinessLayer.Services
{
    public class TorrentsServiceTests
    {
        private readonly ITorrentsService _torrentsService;
        public TorrentsServiceTests()
        {
            var options = new DbContextOptionsBuilder<TorrentsContext>().Options;
            var catalogContextMock = new DbContextMock<TorrentsContext>(options);

            catalogContextMock.CreateDbSetMock(x => x.Forums, InitialEntities.Forums);
            catalogContextMock.CreateDbSetMock(x => x.Torrents, InitialEntities.Torrents);
            catalogContextMock.CreateDbSetMock(x => x.Files, InitialEntities.Files);

            var torrentsRepository = new TorrentsRepository(catalogContextMock.Object);
            _torrentsService = new TorrentsService(torrentsRepository);
        }

        #region GetTorrent(id)_Tests
        [Fact]
        public async Task GetTorrent_3_ReturnExpectedTorrent()
        {
            // Arrange
            const int id = 3;

            // Act
            var result = await _torrentsService.GetTorrent(id);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Id == id, $"Current id ={result.Id} doesn't match expected id={id})");
        }

        [Fact]
        public async Task GetTorrent_InvalidIdentifier_ReturnExceptionWithEventInvalidParameters()
        {
            // Arrange
            const int id = -1;

            // Act
            var exception = await Assert.ThrowsAsync<AppException>(async () => await _torrentsService.GetTorrent(id));

            // Assert
            Assert.Equal(ExceptionEvent.InvalidParameters, exception.ExceptionEvent);
            Assert.Equal("Id can't be negative", exception.Message);
        }

        [Fact]
        public async Task GetTorrent_NonExistingId_ReturnExceptionWithEventNotFound()
        {
            // Arrange
            const int id = 200;

            // Act
            var exception = await Assert.ThrowsAsync<AppException>(async () => await _torrentsService.GetTorrent(id));

            // Assert
            Assert.Equal(ExceptionEvent.NotFound, exception.ExceptionEvent);
            Assert.Equal($"Torrent(id={id}) not found", exception.Message);
        }
        #endregion

        #region GetTorrentsAndCount(pageIndex, itemsPage, search, forumId, sizeFrom, sizeTo, dateFrom, dateTo)

        public static TheoryData<int, int, string, int?, int?, int?, DateTimeOffset?, DateTimeOffset?, int, int>
            DataForGetTorrentsAndCount_FilterAndPaginationParameters =>
            new TheoryData<int, int, string, int?, int?, int?, DateTimeOffset?, DateTimeOffset?, int, int> {
                { 0, 5, null, null, null, null, null, null, 5, 11},
                { 1, 2, null, null, null, null, null, null, 2, 11},
                { 0, 4, "Torrent", 2, int.MinValue, int.MaxValue, DateTimeOffset.MinValue, DateTimeOffset.MaxValue, 4, 5}
            };

        [Theory]
        [MemberData(nameof(DataForGetTorrentsAndCount_FilterAndPaginationParameters))]
        public async Task GetTorrentsAndCount_FilterAndPaginationParameters_ReturnTorrentsAndCount(int pageIndex, int itemsPage,
            string search, int? forumId, long? sizeFrom, long? sizeTo, DateTimeOffset? dateFrom, DateTimeOffset? dateTo,
            int expectedCount, int expectedTotalItems)
        {
            //Act
            var (torrents, count) = await _torrentsService.GetTorrentsAndCount(pageIndex, itemsPage, search, forumId,
                sizeFrom, sizeTo, dateFrom, dateTo);

            //Assert
            Assert.NotNull(torrents);
            Assert.True(expectedCount == torrents.Count(),
                $"Current count={torrents.Count()} doesn't match expected count={expectedCount}");
            Assert.True(expectedTotalItems == count,
                $"Current totalItems={count} doesn't match expected totalItems={expectedTotalItems}");
        }

        public static TheoryData<int, int, string, int?, int?, int?, DateTimeOffset?, DateTimeOffset?, ExceptionEvent, string>
            DataForGetTorrentsAndCount_ParametersCausingErrors =>
            new TheoryData<int, int, string, int?, int?, int?, DateTimeOffset?, DateTimeOffset?, ExceptionEvent, string> {
                { -1, 5, null, null, null, null, null, null, ExceptionEvent.InvalidParameters,"Page can't be negative"},
                { 1, -1, null, null, null, null, null, null, ExceptionEvent.InvalidParameters, "Number of elements per page can't be negative"},
                { 0, 4, "Torrent", 2, 0, 0, DateTimeOffset.MinValue, DateTimeOffset.MaxValue,ExceptionEvent.NotFound,"Torrents not found"}
            };

        [Theory]
        [MemberData(nameof(DataForGetTorrentsAndCount_ParametersCausingErrors))]
        public async Task GetTorrentsAndCount_ParametersCausingErrors_ReturnValidError(int pageIndex, int itemsPage,
            string search, int? forumId, long? sizeFrom, long? sizeTo, DateTimeOffset? dateFrom, DateTimeOffset? dateTo,
            ExceptionEvent exceptionEvent, string exceptionMessage)
        {
            // Assert & Act
            var exception = await Assert.ThrowsAsync<AppException>(async () =>
                await _torrentsService.GetTorrentsAndCount(pageIndex, itemsPage, search, forumId,
                    sizeFrom, sizeTo, dateFrom, dateTo));

            // Assert
            Assert.Equal(exceptionEvent, exception.ExceptionEvent);
            Assert.Equal(exceptionMessage, exception.Message);
        }

        #endregion

        #region GetDataToFilter(int count)_Tests

        [Fact]
        public async Task GetDataToFilter_2_999999999_ReturnTheRequestedNumberOfForumsAndMaxTorrentSize()
        {
            // Arrange
            const int expectedForumsCount = 2;
            const int expectedMaxTorrentSize = 999999999;

            // Act
            var (forums, maxTorrentSize) = await _torrentsService.GetDataToFilter(expectedForumsCount);

            // Assert
            Assert.NotNull(forums);
            Assert.NotNull(forums);
            Assert.True(expectedForumsCount == forums.Count(),
                $"Current count={forums.Count()} doesn't match expected count={expectedForumsCount})");
            Assert.True(expectedMaxTorrentSize == maxTorrentSize,
                $"Current maxTorrentSize={maxTorrentSize} doesn't match expected maxTorrentSize={expectedMaxTorrentSize})");
        }

        [Fact]
        public async Task GetDataToFilter_InvalidParameter_ReturnExceptionWithEventInvalidParameters()
        {
            // Arrange
            const int count = -1;

            // Act
            var exception = await Assert.ThrowsAsync<AppException>(async () =>
                await _torrentsService.GetDataToFilter(count));

            // Assert
            Assert.Equal(ExceptionEvent.InvalidParameters, exception.ExceptionEvent);
            Assert.Equal("forumsCount can't be negative", exception.Message);
        }

        #endregion

    }
}
