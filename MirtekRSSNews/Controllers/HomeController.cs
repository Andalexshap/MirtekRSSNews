using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MirtekRSSNews.Interfaces;
using MirtekRSSNews.Models;

namespace MirtekRSSNews.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRssNewsService _rssNewsService;
        private readonly IRssChanelsService _rssChanelsService;

        public HomeController(IRssNewsService rssNewsService, IRssChanelsService rssChanelsService)
        {
            _rssNewsService = rssNewsService;
            _rssChanelsService = rssChanelsService;
        }

        public IActionResult Index()
        {
            var model = _rssNewsService.GetRSSNews();

            return View(model);
        }

        public IActionResult Search(string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                return RedirectToAction("Index");
            }
            var model = _rssNewsService.GetRSSNews().Where(x => x.Title.Contains(searchString));
            return View(model);
        }

        public IActionResult NewsEdit(Guid id)
        {
            RSSNews model = id == default ? new RSSNews() : _rssNewsService.GetRSSNews(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult NewsDelete(Guid id)
        {
            _rssNewsService.DeleteRSSNews(new RSSNews { Id = id });
            return RedirectToAction("Index");
        }

        [HttpGet("AddRssAddress")]
        public IActionResult AddRssAddress()
        {
            var rssChanels = _rssNewsService.GetRssUrls();
            return View("AddRssAddress", rssChanels);
        }

        [HttpPost("SaveRssAddress")]
        public IActionResult SaveRssAddress(RSSUrl model)
        {
            _rssNewsService.SaveRssUrl(model);

            return RedirectToAction("AddRssAddress");
        }

        [HttpPost("AddDefaultRssAddress")]
        public IActionResult AddDefaultRssAddress()
        {
            _rssChanelsService.SetDefaultRssChanel();

            return RedirectToAction("AddRssAddress");
        }

        [HttpGet]
        public async Task<IActionResult> ParseUrlAddress()
        {
            var rssChanels = _rssNewsService.GetRssUrls();
            foreach (var rssChanel in rssChanels)
            {
                await _rssChanelsService.ParseRssUrl(rssChanel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RssDelete(Guid id)
        {
            _rssNewsService.DeleteRSSUrl(new RSSUrl { Id = id });
            return RedirectToAction("AddRssAddress");
        }
    }
}