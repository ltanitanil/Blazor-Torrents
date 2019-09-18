using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Blazor.Server.BusinessLayer.Services.TorrentsService;
using Blazor.Shared.ViewModels;
using Blazor.Shared.ViewModels.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Blazor.Server.DataAccessLayer.Entities;
using Blazor.Shared.Models.ViewModels.TorrentModel;

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
            var (torrents, count) = await _torrentsService.GetTorrentsAndCount(pageIndex, Constants.ITEMS_PER_PAGE, criteria.SearchText, criteria.SubcategoryId,
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
            var (subcategories, maxTorrentSize) = await _torrentsService.GetDataToFilter(Constants.FORUMS_PER_PAGE);

            return new SearchAndFilterData
            {
                Subcategory = _mapper.Map<SubcategoryView[]>(subcategories),
                TorrentMaxSize = maxTorrentSize
            };
        }

        [HttpGet]
        public async Task<IReadOnlyList<CategoryView>> GetCategoriesWithSubcategories() => 
            _mapper.Map<CategoryView[]>(await _torrentsService.GetCategoriesWithSubcategories());

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task UploadTorrent([FromForm]string json, IEnumerable<IFormFile> files) =>
            await _torrentsService.UploadTorrent(_mapper.Map<Torrent>(JsonSerializer.Deserialize<TorrentUploadViewModel>(json)), 
                files, User.Identity.Name);

        [Authorize(Roles = "User")]
        [HttpDelete("{id}")]
        public async Task DeleteTorrent(int id)
            => await _torrentsService.DeleteTorrent(id, User.Identity.Name);

        [Authorize(Roles = "User")]
        [HttpGet("{directoryName}/{fileName}")]
        public IActionResult DownloadTorrent(string directoryName, string fileName)
        {
            return Redirect(_torrentsService.GetLinkToDownloadFile(directoryName, fileName));
        }
    }
}