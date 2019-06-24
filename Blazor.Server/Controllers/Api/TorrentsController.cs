using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Server.Interfaces;
using Blazor.Shared.ViewModels.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Server.Controllers.Api
{
    public class TorrentsController : BaseController
    {
        private readonly ITorrentsViewModelService _torrentsViewModelService;

        public TorrentsController(ITorrentsViewModelService torrentsViewModelService)
        {
            _torrentsViewModelService = torrentsViewModelService;
        }

        [HttpPost]
        public async Task<IActionResult> GetTorrents([FromBody] SearchAndFilterCriteria criteria, int? pageIndex)
        {
            var torrents = await _torrentsViewModelService.GetTorrents(pageIndex ?? 0, Constants.ITEMS_PER_PAGE, criteria);
            return Ok(torrents);
        }

        [HttpGet]
        public async Task<IActionResult> GetTorrent(int id)
        {
            var torrent = await _torrentsViewModelService.GetTorrent(id);
            return Ok(torrent);
        }

        [HttpGet]
        public async Task<IActionResult> GetDataToFilter()
        {
            var popularForums = await _torrentsViewModelService.GetDataToFilter(Constants.FORUMS_PER_PAGE);
            return Ok(popularForums);
        }
    }
}