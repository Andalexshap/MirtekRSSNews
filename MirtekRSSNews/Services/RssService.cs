using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;
using MirtekRSSNews.Interfaces;
using MirtekRSSNews.Models;

namespace MirtekRSSNews.Services
{
    public class RssService : IRssService
    {
        private readonly IRssNews _rssNews;
        private readonly IConfiguration configuration;
        public RssService(IRssNews rssNews, IConfiguration configuration)
        {
            this.configuration = configuration;
            _rssNews = rssNews;
        }
        public void ParseRssUrl(UrlRssAdress url)
        {
            string parseFormat = "ddd, dd MMM yyyy, HH:mm:ss zzz";

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
                _rssNews.SaveRSSNews(ListOfNews);
            }
        }
        public void SetDefaultRssChanel()
        {
            var listUrl = new List<UrlRssAdress>();

            var urlRssAdress = new UrlRssAdress
            {
                Id = new Guid(),
                Url = configuration["RssCHanel:Yandex"]
            };
            listUrl.Add(urlRssAdress);
            urlRssAdress = new UrlRssAdress
            {
                Id = new Guid(),
                Url = configuration["RssCHanel:Mchs"]
            };
            listUrl.Add(urlRssAdress);
            _rssNews.SaveUrlRssAdress(listUrl);
        }

    }
}
