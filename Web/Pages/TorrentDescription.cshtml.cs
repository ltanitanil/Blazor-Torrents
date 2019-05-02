using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels.Torrent;

namespace Web.Pages
{
    public class TorrentDescriptionModel : PageModel
    {
        public readonly ITorrentsViewModelService _torrentViewModelService;

        public TorrentDescriptionModel(ITorrentsViewModelService torrentViewModelService)
        {
            _torrentViewModelService = torrentViewModelService;
        }

        public TorrentDescriptionViewModel TorrentModel { get; set; } = new TorrentDescriptionViewModel();

        public async Task OnGet(int id)
        {
            TorrentModel = await _torrentViewModelService.GetTorrent(id);
        }
    }
}