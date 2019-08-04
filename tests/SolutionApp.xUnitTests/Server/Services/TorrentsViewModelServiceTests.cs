using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blazor.Core.Entities;
using Blazor.Core.Interfaces;
using Blazor.Server.Exceptions;
using Blazor.Server.Interfaces;
using Blazor.Server.Services;
using Blazor.Shared.ViewModels.Search;
using Blazor.Shared.ViewModels.TorrentModel;
using Castle.Core.Internal;
using Moq;
using Tests.Shared;
using Xunit;

namespace SolutionApp.xUnitTests.Server.Services
{
    public class TorrentsViewModelServiceTests
    {
        private readonly ITorrentsViewModelService _torrentsService;

        public TorrentsViewModelServiceTests()
        {
            var torrentsRepository = GetITorrentRepository();
            var mapper = GetIMapper();

            _torrentsService = new TorrentsViewModelService(mapper, torrentsRepository);
        }


        #region GetTorrent(int id)_Tests
        [Fact]
        public async Task GetTorrent_Return_Existing_Torrent_By_Id()
        {
            const int id = 3;

            var result = await _torrentsService.GetTorrent(id);

            Assert.NotNull(result);
            Assert.True(result.Id == id, $"Current id ={result.Id} doesn't match expected id={id})");
        }

        [Fact]
        public async Task GetTorrent_Return_Exception_If_Id_Does_Not_Exist()
        {
            const int id = -1;

            var exception = await Assert.ThrowsAsync<ApiTorrentsException>(async () => await _torrentsService.GetTorrent(id));

            Assert.Equal(ExceptionEvent.NotFound, exception.ExceptionEvent);
            Assert.Equal($"Torrent(id={id}) not found", exception.Message);
        }
        #endregion

        #region GetTorrents(int pageIndex,int itemsPage,SearchAndFilterCriteria criteria)_Tests

        public static TheoryData<int, int, SearchAndFilterCriteria, int> DataForGetTorrents =>
            new TheoryData<int, int, SearchAndFilterCriteria, int> {
                { 0, 5, new SearchAndFilterCriteria(), 5},
                { 1,2,new SearchAndFilterCriteria(), 2}
            };

        [Theory]
        [MemberData(nameof(DataForGetTorrents))]
        public async Task GetTorrents_Return_Existing_Torrents(int pageIndex, int itemsPage, SearchAndFilterCriteria criteria, int expectedCount)
        {
            var result = await _torrentsService.GetTorrents(pageIndex, itemsPage, criteria);

            Assert.NotNull(result);
            Assert.NotNull(result.Torrents);
            Assert.NotNull(result.PaginationInfo);
            Assert.True(expectedCount == result.Torrents.Count(), $"Current count={result.Torrents.Count()} doesn't match expected count={expectedCount}");
            Assert.True(pageIndex == result.PaginationInfo.CurrentPage, $"Current page={result.PaginationInfo.CurrentPage} doesn't match expected page={pageIndex}");
        }

        [Fact]
        public async Task GetTorrents_Return_Exception_If_if_The_Received_TorrentList_Is_Null()
        {
            var exception = await Assert.ThrowsAsync<ApiTorrentsException>(async () =>
                await _torrentsService.GetTorrents(100, 100, new SearchAndFilterCriteria()));

            Assert.Equal(ExceptionEvent.NotFound, exception.ExceptionEvent);
            Assert.Equal($"Not found", exception.Message);
        }

        #endregion

        #region GetDataToFilter(int count)_Tests

        [Fact]
        public async Task GetDataToFilter_Return_The_Requested_Number_Of_Forums()
        {
            const int forumsCount = 2;

            var result = await _torrentsService.GetDataToFilter(forumsCount);

            Assert.NotNull(result);
            Assert.NotNull(result.Forums);
            Assert.True(result.Forums.Count() == forumsCount, $"Current count={result.Forums.Count()} doesn't match expected count={forumsCount})");
        }

        #endregion

        #region Setting for torrentsRepositoryMock
        public static ITorrentsRepository GetITorrentRepository()
        {
            var torrentsRepositoryMock = new Mock<ITorrentsRepository>();

            torrentsRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int x) => InitialEntities.Torrents.FirstOrDefault(y => y.Id == x));

            torrentsRepositoryMock.Setup(x => x.ListAsync(It.IsAny<ISpecification<Torrent>>()))
                .ReturnsAsync((ISpecification<Torrent> spec) =>
                {
                    var torrents = InitialEntities.Torrents.AsQueryable().Where(spec.Criteria)
                        .Skip(spec.Skip)
                        .Take(spec.Take)
                        .ToList();
                    return torrents.IsNullOrEmpty() ? null : torrents;
                });

            torrentsRepositoryMock.Setup(x => x.CountAsync(It.IsAny<ISpecification<Torrent>>()))
                .ReturnsAsync((ISpecification<Torrent> spec) =>
                    InitialEntities.Torrents.AsQueryable().Where(spec.Criteria).Count());

            torrentsRepositoryMock.Setup(x => x.GetPopularForumsAsync(It.IsAny<int>()))
                .ReturnsAsync((int forumsCount) => InitialEntities.Torrents
                    .GroupBy(x => x.ForumId, (key, items) => new { Key = key, Count = items.Count() })
                    .OrderByDescending(x => x.Count)
                    .Take(forumsCount)
                    .Join(InitialEntities.Forums, (t) => t.Key, (f) => f.Id, (t, f) => f)
                    .ToList());

            torrentsRepositoryMock.Setup(x => x.GetMaxTorrentSizeAsync())
                .ReturnsAsync(InitialEntities.Torrents.Max(x => x.Size));

            return torrentsRepositoryMock.Object;
        }
        #endregion

        #region Setting for mapperMock
        public static IMapper GetIMapper()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(x => x.Map<TorrentDescriptionView>(It.IsAny<Torrent>()))
                .Returns<Torrent>(torrent => new TorrentDescriptionView { Id = torrent.Id });
            mapperMock.Setup(x => x.Map<TorrentView[]>(It.IsAny<IReadOnlyList<Torrent>>()))
                .Returns<IReadOnlyList<Torrent>>(torrents => torrents.Select(x => new TorrentView { Id = x.Id }).ToArray());
            mapperMock.Setup(x => x.Map<ForumView[]>(It.IsAny<IReadOnlyList<Forum>>()))
                .Returns<IReadOnlyList<Forum>>(forums => forums.Select(x => new ForumView { Id = x.Id }).ToArray());

            return mapperMock.Object;
        }

        #endregion
    }
}
