using RssReader.Services.Models;
using RssReader.Services.Models.Interfaces;
using RssReader.Services.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RssReader.WebMVC.Controllers
{
    public class RSSController : Controller
    {
        private readonly IRssServices _rssServices;

        public RSSController(IRssServices rssServices)
        {
            _rssServices = rssServices;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var dbCannels = await _rssServices.GetChannelsAsync();

            SelectList channels = new SelectList(dbCannels, "Id", "RssSourceLink");

            ViewBag.Channels = channels;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PageListPartial(PostView postView)
        {
            
            var pagingPostsList = await _rssServices.GetPagingPostsListAsync(postView);

            return PartialView(pagingPostsList);
        }
    }
}