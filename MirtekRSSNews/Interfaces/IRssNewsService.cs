using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MirtekRSSNews.Models;

namespace MirtekRSSNews.Interfaces
{
    public interface IRssNewsService
    {
        public IQueryable<RSSNews> GetRSSNews();
        public RSSNews GetRSSNews(Guid id);
        public Task SaveRSSNews(List<RSSNews> entity);
        public Task DeleteRSSNews(RSSNews entity);
        public IQueryable<RSSUrl> GetRssUrls();
        public Task<Guid> SaveRssUrl(RSSUrl entity);
        public Task SaveRssUrls(List<RSSUrl> entity);

        public Task DeleteRSSUrl(RSSUrl entity);
    }
}