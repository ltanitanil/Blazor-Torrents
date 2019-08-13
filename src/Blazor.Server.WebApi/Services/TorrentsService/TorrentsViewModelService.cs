using AutoMapper;
using Blazor.Shared.ViewModels;
using Blazor.Shared.ViewModels.Search;
using Blazor.Shared.ViewModels.TorrentModel;
using System.Threading.Tasks;
using Blazor.Server.BusinessLayer.Services.TorrentsService;
using Blazor.Server.WebApi.Exceptions;

namespace Blazor.Server.WebApi.Services.TorrentsService
{
    public class TorrentsViewModelService : ITorrentsViewModelService
    {
        private readonly IMapper _mapper;
        private readonly ITorrentsService _torrentService;

        public TorrentsViewModelService(IMapper mapper, ITorrentsService torrentService)
        {
            _mapper = mapper;
            _torrentService = torrentService;
        }

        public async Task<TorrentsViewModel> GetTorrents(int pageIndex, int itemsPage, SearchAndFilterCriteria criteria)
        {
            var torrentsOnPage = await _torrentService.GetTorrents(itemsPage * pageIndex, itemsPage, criteria.SearchText, 
                criteria.SelectedForumId, criteria.Size.From, criteria.Size.To, criteria.Date.From, criteria.Date.To)
                                 ?? throw new ApiTorrentsException(ExceptionEvent.NotFound, $"Torrents not found");

            var totalTorrents = await _torrentService.GetTorrentsCount(criteria.SearchText, criteria.SelectedForumId,
                criteria.Size.From, criteria.Size.To, criteria.Date.From, criteria.Date.To);

            return new TorrentsViewModel
            {
                Torrents = _mapper.Map<TorrentView[]>(torrentsOnPage),
                PaginationInfo = new PaginationInfoViewModel(totalTorrents, pageIndex, itemsPage, 5) // 5 пока оставила как есть(потом вовсе уберу)
            };

        }

        public async Task<TorrentDescriptionView> GetTorrent(int id)
        {
            var torrent = await _torrentService.GetTorrent(id)
                          ?? throw new ApiTorrentsException(ExceptionEvent.NotFound, $"Torrent(id={id}) not found");

            return _mapper.Map<TorrentDescriptionView>(torrent);
        }

        public async Task<SearchAndFilterData> GetDataToFilter(int forumsCount)
        {
            var forums = await _torrentService.GetDataToFilter(forumsCount);

            return new SearchAndFilterData
            {
                Forums = _mapper.Map<ForumView[]>(forums.Item1),
                TorrentMaxSize = forums.Item2
            };
        }
    }
}
