using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels;
using Web;
using Web.ViewModels.Torrent;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        public readonly ITorrentsViewModelService _catalogViewModelService;

        public IndexModel(ITorrentsViewModelService catalogViewModelService)
        {
            _catalogViewModelService = catalogViewModelService;
        }

        public TorrentsViewModel CatalogModel { get; set; } = new TorrentsViewModel();

        public async Task OnGet(TorrentsViewModel catalogModel, int? pageId)
        {
            CatalogModel = await _catalogViewModelService.GetTorrents(pageId ?? 0, Constants.ITEMS_PER_PAGE, catalogModel.SearchText);
        }
    }
}