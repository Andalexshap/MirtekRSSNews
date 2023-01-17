using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MirtekRSSNews.Interfaces;
using MirtekRSSNews.Models;

namespace MirtekRSSNews.Services
{
    public class NewsRepository : IRssNews
    {
        private readonly AppDbContext context;

        public NewsRepository(AppDbContext context)
        {

            this.context = context;
        }
        public IQueryable<RSSNews> GetRSSNews()
        {
            var response = context.MirtekRSSNews.OrderBy(x => x.Title);

            return response;
        }
        public RSSNews GetRSSNews(Guid id)
        {
            return context.MirtekRSSNews.Single(x => x.Id == id);
        }

        public Guid SaveRSSNews(RSSNews entity)
        {
            if (entity.Id == default)
            {
                context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                context.Entry(entity).State = EntityState.Modified;
            }

            context.SaveChanges();

            return entity.Id;
        }
        public void SaveRSSNews(List<RSSNews> entity)
        {
            context.AddRange(entity);
            context.SaveChanges();

        }
        public void DeleteRSSNews(RSSNews entity)
        {
            context.MirtekRSSNews.Remove(entity);
            context.SaveChanges();
        }
        public IQueryable<UrlRssAdress> GetUrlRssAdress()
        {
            return context.UrlRssAdresses;
        }
        public Guid SaveUrlRssAdress(UrlRssAdress entity)
        {
            context.Add(entity);
            context.SaveChanges();

            return entity.Id;
        }
        public void SaveUrlRssAdress(List<UrlRssAdress> entity)
        {
            context.AddRange(entity);
            context.SaveChanges();
        }
        public void DeleteRSSChanel(UrlRssAdress entity)
        {
            context.UrlRssAdresses.Remove(entity);
            context.SaveChanges();
        }
    }

}
