using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MirtekRSSNews.Interfaces;
using MirtekRSSNews.Models;

namespace MirtekRSSNews.Services
{
    public class RssNewsService : IRssNewsService
    {
        private readonly AppDbContext _context;

        public RssNewsService(AppDbContext context)
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
            return _context.MirtekRSSNews.FirstOrDefault(x => x.Id == id);
        }

        public async Task SaveRSSNews(List<RSSNews> entity)
        {
            if (entity.Any())
            {
                await _context.AddRangeAsync(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteRSSNews(RSSNews entity)
        {
            _context.MirtekRSSNews.Remove(entity);
            await _context.SaveChangesAsync();
        }

        //Rss chanels
        public IQueryable<RSSUrl> GetRssUrls()
        {
            return _context.UrlRssAdresses;
        }

        public async Task<Guid> SaveRssUrl(RSSUrl entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task SaveRssUrls(List<RSSUrl> entity)
        {
            await _context.AddRangeAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRSSUrl(RSSUrl entity)
        {
            _context.UrlRssAdresses.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

}