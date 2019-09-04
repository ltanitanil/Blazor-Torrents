using System.Threading.Tasks;
using AutoMapper;
using Blazor.Server.BusinessLayer.Services.TorrentsService;
using Blazor.Shared.ViewModels;
using Blazor.Shared.ViewModels.Search;
using Blazor.Shared.ViewModels.TorrentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Server.WebApi.Controllers.Api
{
    public class TorrentsController : BaseApiController
    {
        private readonly ITorrentsService _torrentsService;

        public TorrentsController(IMapper mapper, ITorrentsService torrentsViewModelService) : base(mapper)
        {
            _torrentsService = torrentsViewModelService;
        }

        [HttpPost]
        public async Task<TorrentsViewModel> GetTorrents(int pageIndex, SearchAndFilterCriteria criteria)
        {
            var (torrents, count) = await _torrentsService.GetTorrentsAndCount(pageIndex, Constants.ITEMS_PER_PAGE, criteria.SearchText, criteria.SelectedForumId,
                criteria.Size.From, criteria.Size.To, criteria.Date.From, criteria.Date.To);

            return new TorrentsViewModel
            {
                Torrents = _mapper.Map<TorrentView[]>(torrents),
                PaginationInfo = new PaginationInfoViewModel
                {
                    TotalItems = count,
                    CurrentPage = pageIndex,
                    PageSize = Constants.ITEMS_PER_PAGE
                }
            };
        }
        [Authorize]
        [HttpGet]
        public async Task<TorrentDescriptionView> GetTorrent(int id) =>
                    _mapper.Map<TorrentDescriptionView>(await _torrentsService.GetTorrent(id));

        [HttpGet]
        public async Task<SearchAndFilterData> GetDataToFilter()
        {
            var (forums, maxTorrentSize) = await _torrentsService.GetDataToFilter(Constants.FORUMS_PER_PAGE);

            return new SearchAndFilterData
            {
                Forums = _mapper.Map<ForumView[]>(forums),
                TorrentMaxSize = maxTorrentSize
            };
        }
    }
}