using Blazor.Shared.ViewModels.Search;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Blazor.Shared.ViewModels;
using Blazor.Shared.ViewModels.TorrentModel;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Blazor.Tests.IntegrationTests.Blazor.Server.WebApi
{
    public class ApiTorrentsControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ApiTorrentsControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        #region GetTorrent()

        [Theory]
        [InlineData(1)]
        public async Task GetTorrent_1_ReturnTheRequestedTorrent(int id)
        {
            //Act
            var torrent = await _client.GetJsonAsync<TorrentDescriptionView>($"api/Torrents/GetTorrent/?id={id}");

            //Assert
            Assert.NotNull(torrent);
            Assert.True(id == torrent.Id,
                $"Expected id={id} doesn't match the actual id={torrent.Id}");
            Assert.NotNull(torrent.Forum);
            Assert.NotNull(torrent.Files);
        }

        [Theory]
        [InlineData(2222222)]
        public async Task GetTorrent_NonExistingId_ReturnNotFound(int id)
        {
            //Act
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<TorrentDescriptionView>($"api/Torrents/GetTorrent/?id={id}"));

            //Assert
            Assert.True("Response status code does not indicate success: 404 (Not Found)." == exception.Message,
                $"Expected 404 (Not Found) error code, actual - {exception.Message}");
        }

        [Theory]
        [InlineData(-20)]
        public async Task GetTorrent_InvalidId_ReturnBadRequest(int invalidId)
        {
            //Act
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.GetJsonAsync<TorrentDescriptionView>($"api/Torrents/GetTorrent/?id={invalidId}"));

            //Assert
            Assert.True("Response status code does not indicate success: 400 (Bad Request)." == exception.Message,
                $"Expected 400(Bad Request) error code, actual - {exception.Message}");
        }

        #endregion

        #region GetTorrents()

        [Theory]
        [InlineData(0, 10)]
        [InlineData(1, 1)]
        public async Task GetTorrents_ValidParameters_ReturnTheRequestedTorrentPage(int pageIndex, int expectedCount)
        {
            //Arrange
            var criteria = new SearchAndFilterCriteria();

            //Act
            var torrents = await _client.PostJsonAsync<TorrentsViewModel>($"/api/Torrents/GetTorrents/?pageIndex={pageIndex}", criteria);

            //Assert
            Assert.NotNull(torrents);
            Assert.NotNull(torrents.Torrents);
            Assert.NotNull(torrents.PaginationInfo);
            Assert.True(expectedCount == torrents.Torrents.Count(),
                $"Expected count={expectedCount} doesn't match the actual count={torrents.Torrents.Count()}");
            Assert.True(pageIndex == torrents.PaginationInfo.CurrentPage,
                $"Expected page={pageIndex} doesn't match the actual page={torrents.PaginationInfo.CurrentPage}");
        }

        [Theory]
        [InlineData(-20, null)]
        public async Task GetTorrents_InvalidParameters_ReturnBadRequest(int pageIndex, SearchAndFilterCriteria criteria)
        {
            //Act
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _client.PostJsonAsync($"/api/Torrents/GetTorrents/?pageIndex={pageIndex}", criteria));

            //Assert
            Assert.True("Response status code does not indicate success: 400 (Bad Request)." == exception.Message,
                $"Expected 400(Bad Request) error code, actual - {exception.Message}");
        }


        #endregion

        #region  GetDataToFilter()

        [Fact]
        public async Task GetDataToFilter_ReturnSearchAndFilterData()
        {
            //Act
            var searchAndFilterData = await _client.GetJsonAsync<SearchAndFilterData>("api/Torrents/GetDataToFilter");

            //Assert
            Assert.NotNull(searchAndFilterData);
        }

        #endregion

    }
}
