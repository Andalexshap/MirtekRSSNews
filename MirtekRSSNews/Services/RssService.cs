using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using MirtekRSSNews.Interfaces;
using MirtekRSSNews.Models;

namespace MirtekRSSNews.Services
{
    public class RssService : IRssService
    {
        private readonly NewsRepository _newsRepository;
        public RssService(NewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }
        public void ParseRssUrl(UrlRssAdress url)
        {
            string parseFormat = "ddd, dd MMM yyyy HH:mm:ss zzz";

            XDocument rssXmlDoc = new XDocument();
            XNamespace yandex = "http://news.yandex.ru";
            rssXmlDoc = XDocument.Load(url.Url);
            var items = (from a in rssXmlDoc.Descendants("item")
                         select new
                         {
                             title = a.Element("title").Value,
                             text = a.Element(yandex + "full-text").Value,
                             data = a.Element("pubDate").Value,
                         });
            if (items != null)
            {
                var ListOfNews = new List<RSSNews>();
                foreach (var i in items)
                {
                    RSSNews news = new RSSNews
                    {
                        Id = new Guid(),
                        Title = i.title,
                        Text = i.text,
                        DateOfNews = DateTime.ParseExact(i.data, parseFormat,
                                                      CultureInfo.InvariantCulture)
                    };
                    ListOfNews.Add(news);
                }
                _newsRepository.SaveRSSNews(ListOfNews);
            }
        }

    }
}
