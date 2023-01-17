using System;
using System.Collections.Generic;
using System.Linq;
using MirtekRSSNews.Models;

namespace MirtekRSSNews.Interfaces
{
    public interface IRssNews
    {
        public IQueryable<RSSNews> GetRSSNews();
        public RSSNews GetRSSNews(Guid id);
        public Guid SaveRSSNews(RSSNews entity);
        public void SaveRSSNews(List<RSSNews> entity);
        public void DeleteRSSNews(RSSNews entity);
        public IQueryable<UrlRssAdress> GetUrlRssAdress();
        public Guid SaveUrlRssAdress(UrlRssAdress entity);
        public void DeleteRSSChanel(UrlRssAdress entity);
    }
}
