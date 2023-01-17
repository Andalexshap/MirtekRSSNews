using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MirtekRSSNews.Interfaces;
using MirtekRSSNews.Models;
using MirtekRSSNews.Services;

namespace MirtekRSSNews.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRssNews _rssNews;
        private readonly IRssService _rssService;

        public HomeController(NewsRepository newsRepository, IRssService rssService)
        {
            _rssNews = newsRepository;
            _rssService = rssService;
        }

        public IActionResult Index()
        {
            var model = _rssNews.GetRSSNews();
            return View(model);
        }
        public IActionResult Search(string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                return RedirectToAction("Index");
            }
            var model = _rssNews.GetRSSNews().Where(x => x.Title.Contains(searchString));
            return View(model);
        }

        public IActionResult NewsEdit(Guid id)
        {
            RSSNews model = id == default ? new RSSNews() : _rssNews.GetRSSNews(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult NewsEdit(RSSNews model)
        {
            if (ModelState.IsValid)
            {
                _rssNews.SaveRSSNews(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult NewsDelete(Guid id)
        {
            _rssNews.DeleteRSSNews(new RSSNews { Id = id });
            return RedirectToAction("Index");
        }

        [HttpGet("AddRssAddress")]
        public IActionResult AddRssAddress()
        {
            var rssChanels = _rssNews.GetUrlRssAdress();
            return View("AddRssAddress", rssChanels);
        }

        [HttpPost("SaveRssAddress")]
        public IActionResult SaveRssAddress(UrlRssAdress model)
        {
            _rssNews.SaveUrlRssAdress(model);

            return RedirectToAction("AddRssAddress");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult ParseUrlAddress()
        {
            var rssChanels = _rssNews.GetUrlRssAdress();
            foreach (var rssChanel in rssChanels)
            {
                _rssService.ParseRssUrl(rssChanel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RssDelete(Guid id)
        {
            _rssNews.DeleteRSSChanel(new UrlRssAdress { Id = id });
            return RedirectToAction("AddRssAddress");
        }
    }
}
