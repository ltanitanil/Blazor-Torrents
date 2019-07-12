using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Server.Exceptions;
using Blazor.Server.Interfaces;
using Blazor.Shared.ViewModels.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Server.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TorrentsController : ControllerBase
    {
        private readonly ITorrentsViewModelService _torrentsViewModelService;

        public TorrentsController(ITorrentsViewModelService torrentsViewModelService)
        {
            _torrentsViewModelService = torrentsViewModelService;
        }

        [HttpPost]
        public async Task<IActionResult> GetTorrents([FromBody] SearchAndFilterCriteria criteria, int pageIndex=0)
        {
            if (pageIndex < 0)
                throw new ApiTorrentsException(ExceptionEvent.InvalidParameters, "Page can't be negative");

            try
            {
                var torrents = await _torrentsViewModelService.GetTorrents(pageIndex, Constants.ITEMS_PER_PAGE, criteria);
                return Ok(torrents);
            }
            catch (NullReferenceException)
            {
                throw new ApiTorrentsException(ExceptionEvent.NotFound, "Not found");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTorrent(int id)
        {
            if (id < 0)
                throw new ApiTorrentsException(ExceptionEvent.InvalidParameters, "Id can't be negative");

            try
            {
                var torrent = await _torrentsViewModelService.GetTorrent(id);
                return Ok(torrent);
            }
            catch(NullReferenceException)
            {
                throw new ApiTorrentsException(ExceptionEvent.NotFound, $"Torrent(id={id}) not found");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDataToFilter()
        {
            var popularForums = await _torrentsViewModelService.GetDataToFilter(Constants.FORUMS_PER_PAGE);
            return Ok(popularForums);
        }
    }
}