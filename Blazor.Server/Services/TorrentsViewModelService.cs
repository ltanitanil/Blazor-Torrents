using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using AutoMapper;
using Blazor.Server.Exceptions;
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
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Torrent> _torrentRepository;
        private readonly IAsyncRepository<Forum> _forumRepository;

        public TorrentsViewModelService(IMapper mapper,IAsyncRepository<Torrent> torrentRepository,
            IAsyncRepository<Forum> forumRepository)
        {
            _mapper = mapper;
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

            var torrentsOnPage = await _torrentRepository.ListAsync(filterPaginatedSpecification) ?? throw new ApiTorrentsException(ExceptionEvent.NotFound, "Not found");
            var totalTorrents = await _torrentRepository.CountAsync(filterSpecification);

            return new TorrentsViewModel
            {
                Torrents= _mapper.Map<TorrentView[]>(torrentsOnPage),
                PaginationInfo = new PaginationInfoViewModel(totalTorrents, pageIndex, 10, 5)
            };

        }

        public async Task<TorrentDescriptionView> GetTorrent(int id)
        {
            var torrent = await _torrentRepository.GetByIdAsync(id) ?? throw new ApiTorrentsException(ExceptionEvent.NotFound, $"Torrent(id={id}) not found");

            return _mapper.Map<TorrentDescriptionView>(torrent);
        }

        public async Task<SearchAndFilterData> GetDataToFilter(int forumsCount)
        {
            var forumsId = await _torrentRepository.GetPopularEntriesAsync(forumsCount, x => x.ForumId);
            var forumsList = await _forumRepository.GetListByIDsAsync(forumsId);

            return new SearchAndFilterData
            {
                Forums = _mapper.Map<ForumView[]>(forumsList),
                TorrentMaxSize = await _torrentRepository.GetMaxValueAsync(x => x.Size)
            };
        }
    }
}
