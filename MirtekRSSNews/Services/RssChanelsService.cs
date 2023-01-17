using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;
using MirtekRSSNews.Interfaces;
using MirtekRSSNews.Models;

namespace MirtekRSSNews.Services
{
    public class RssChanelsService : IRssChanelsService
    {
        private readonly IRssNewsService _rssNews;
        private readonly IConfiguration _configuration;
        public RssChanelsService(IRssNewsService rssNews, IConfiguration configuration)
        {
            _configuration = configuration;
            _rssNews = rssNews;
        }
        public async Task ParseRssUrl(RSSUrl url)
        {
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
                    try
                    {
                        RSSNews news = new RSSNews
                        {
                            Id = Guid.NewGuid(),
                            Title = i.title,
                            Text = i.text,
                            DateOfNews = DateTime.Parse(i.data)
                        };
                        ListOfNews.Add(news);
                    }
                    catch
                    {
                        continue;
                    }
                }
                await _rssNews.SaveRSSNews(ListOfNews);
            }
        }
        public void SetDefaultRssChanel()
        {
            var listUrl = new List<RSSUrl>();

            var urlRssAdress = new RSSUrl
            {
                Id = new Guid(),
                Url = _configuration["RssCHanel:Yandex"]
            };

            listUrl.Add(urlRssAdress);

            urlRssAdress = new RSSUrl
            {
                Id = new Guid(),
                Url = _configuration["RssCHanel:Mchs"]
            };

            listUrl.Add(urlRssAdress);

            _rssNews.SaveRssUrls(listUrl);
        }

    }
}