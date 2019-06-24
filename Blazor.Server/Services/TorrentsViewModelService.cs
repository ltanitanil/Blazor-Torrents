using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Blazor.Server.Helpers;
using Blazor.Server.Interfaces;
using Blazor.Shared.ViewModels;
using Blazor.Shared.ViewModels.Search;
using Blazor.Shared.ViewModels.TorrentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Server.Services
{
    public class TorrentsViewModelService : ITorrentsViewModelService
    {
        private readonly IAsyncRepository<Torrent> _torrentRepository;
        private readonly IAsyncRepository<Forum> _forumRepository;

        public TorrentsViewModelService(IAsyncRepository<Torrent> torrentRepository,
            IAsyncRepository<Forum> forumRepository)
        {
            _torrentRepository = torrentRepository;
            _forumRepository = forumRepository;
        }

        public async Task<TorrentsViewModel> GetTorrents(int pageIndex, int itemsPage, SearchAndFilterCriteria criteria)
        {
            var filterSpecification = new CatalogFilterSpecification(criteria.SearchText,
                                                                     criteria.SelectedForumId,
                                                                     criteria.Size.From,
                                                                     criteria.Size.To,
                                                                     criteria.Date.From,
                                                                     criteria.Date.To);
            var filterPaginatedSpecification = new CatalogFilterPaginatedSpecification(itemsPage * pageIndex,
                                                                                       itemsPage,
                                                                                       criteria.SearchText,
                                                                                       criteria.SelectedForumId,
                                                                                       criteria.Size.From,
                                                                                       criteria.Size.To,
                                                                                       criteria.Date.From,
                                                                                       criteria.Date.To);
            var torrentsOnPage = await _torrentRepository.ListAsync(filterPaginatedSpecification);
            var totalTorrents = await _torrentRepository.CountAsync(filterSpecification);

            var ci = new TorrentsViewModel
            {
                Torrents = torrentsOnPage.Select(x => new TorrentView()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Size = x.Size,
                    RegistredAt = x.RegistredAt
                }),
                PaginationInfo = new PaginationInfoViewModel(totalTorrents, pageIndex, 10, 10)
            };
            return ci;

        }

        public async Task<TorrentDescriptionView> GetTorrent(int id)
        {
            var torrent = await _torrentRepository.GetByIdAsync(id);
            var t = new TorrentDescriptionView()
            {
                Id = torrent.Id,
                Title = torrent.Title,
                RegistredAt = torrent.RegistredAt,
                Size = torrent.Size,
                Content = BBCodeHelper.Format(torrent.Content),
                Dir_Name = torrent.Dir_Name,
                Forum = new ForumView { Id = torrent.Forum.Id, Value = torrent.Forum.Value },
                Files = torrent.Files.Select(x => new FileView { Name = x.Name, Size = x.Size })
            };
            return t;
        }

        public async Task<SearchAndFilterData> GetDataToFilter(int forumsCount)
        {
            var forumsId = await _torrentRepository.GetPopularEntriesAsync(forumsCount, x => x.ForumId);
            var forumsList = await _forumRepository.GetListByIDsAsync(forumsId);

            SearchAndFilterData data = new SearchAndFilterData()
            {
                Forums = forumsList.Select(x => new ForumView() { Id = x.Id, Value = x.Value }),
                TorrentMaxSize = await _torrentRepository.GetMaxValueAsync(x =>x.Size)
            };

            return data;
        }
    }
}
