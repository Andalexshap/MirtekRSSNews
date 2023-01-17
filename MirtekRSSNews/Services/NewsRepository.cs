using System;
using System.Collections.Generic;
using System.Linq;
using MirtekRSSNews.Interfaces;
using MirtekRSSNews.Models;

namespace MirtekRSSNews.Services
{
    public class NewsRepository : IRssNews
    {
        private readonly AppDbContext _context;

        public NewsRepository(AppDbContext context)
        {

            _context = context;
        }
        public IQueryable<RSSNews> GetRSSNews()
        {
            var response = _context.MirtekRSSNews.OrderBy(x => x.Title);

            return response;
        }
        public RSSNews GetRSSNews(Guid id)
        {
            return _context.MirtekRSSNews.Single(x => x.Id == id);
        }

        public Guid SaveRSSNews(RSSNews entity)
        {
            using (var context = _context)
            {
                context.Add(entity);
                context.SaveChanges();
            }

            return entity.Id;
        }
        public void SaveRSSNews(List<RSSNews> entity)
        {
            using (var context = _context)
            {
                context.AddRange(entity);
                context.SaveChanges();
            }
        }
        public void DeleteRSSNews(RSSNews entity)
        {
            using (var context = _context)
            {
                context.Remove(entity);
                context.SaveChanges();
            }
        }
        public IQueryable<UrlRssAdress> GetUrlRssAdress()
        {
            return _context.UrlRssAdresses;
        }
        public Guid SaveUrlRssAdress(UrlRssAdress entity)
        {
            using (var context = _context)
            {
                context.Add(entity);
                context.SaveChanges();
            }

            return entity.Id;
        }
        public void SaveUrlRssAdress(List<UrlRssAdress> entity)
        {
            using (var context = _context)
            {
                context.AddRange(entity);
                context.SaveChanges();
            }
        }
        public void DeleteRSSChanel(UrlRssAdress entity)
        {
            using (var context = _context)
            {
                context.Remove(entity);
                context.SaveChanges();
            }
        }
    }

}
