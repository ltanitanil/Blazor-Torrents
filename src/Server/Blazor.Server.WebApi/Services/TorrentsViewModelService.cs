using AutoMapper;
using Blazor.Shared.ViewModels;
using Blazor.Shared.ViewModels.Search;
using Blazor.Shared.ViewModels.TorrentModel;
using System.Threading.Tasks;
using Blazor.Server.BusinessLayer.Interfaces;
using Blazor.Server.BusinessLayer.Specifications;
using Blazor.Server.WebApi.Exceptions;
using Blazor.Server.WebApi.Interfaces;

namespace Blazor.Server.WebApi.Services
{
    public class TorrentsViewModelService : ITorrentsViewModelService
    {
        private readonly IMapper _mapper;
        private readonly ITorrentsRepository _torrentRepository;

        public TorrentsViewModelService(IMapper mapper, ITorrentsRepository torrentRepository)
        {
            _mapper = mapper;
            _torrentRepository = torrentRepository;
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

            var torrentsOnPage = await _torrentRepository.ListAsync(filterPaginatedSpecification)
                                 ?? throw new ApiTorrentsException(ExceptionEvent.NotFound, "Not found");
            var totalTorrents = await _torrentRepository.CountAsync(filterSpecification);

            return new TorrentsViewModel
            {
                Torrents = _mapper.Map<TorrentView[]>(torrentsOnPage),
                PaginationInfo = new PaginationInfoViewModel(totalTorrents, pageIndex, itemsPage, 5) // 5 пока оставила как есть(потом вовсе уберу)
            };

        }

        public async Task<TorrentDescriptionView> GetTorrent(int id)
        {
            var torrent = await _torrentRepository.GetByIdAsync(id)
                          ?? throw new ApiTorrentsException(ExceptionEvent.NotFound, $"Torrent(id={id}) not found");

            return _mapper.Map<TorrentDescriptionView>(torrent);
        }

        public async Task<SearchAndFilterData> GetDataToFilter(int forumsCount)
        {
            var forums = await _torrentRepository.GetPopularForumsAsync(forumsCount);

            return new SearchAndFilterData
            {
                Forums = _mapper.Map<ForumView[]>(forums),
                TorrentMaxSize = await _torrentRepository.GetMaxTorrentSizeAsync()
            };
        }
    }
}
