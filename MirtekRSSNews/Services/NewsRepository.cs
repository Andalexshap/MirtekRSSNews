using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MirtekRSSNews.Interfaces;
using MirtekRSSNews.Models;

namespace MirtekRSSNews.Services
{
    public class NewsRepository : IRssNews
    {
        private readonly AppDbContext context;
        private readonly IConfiguration configuration;
        private readonly IRssService rssService;

        public NewsRepository(IConfiguration configuration, AppDbContext context, IRssService rssService)
        {
            this.configuration = configuration;
            this.context = context;
            this.rssService = rssService;
        }
        public IQueryable<RSSNews> GetRSSNews()
        {
            var response = context.MirtekRSSNews.OrderBy(x => x.Title);
            if (response.Count() == 0)
            {
                SetDefaultRssChanel();
                response = context.MirtekRSSNews.OrderBy(x => x.Title);
            }
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
            var response = GetUrlRssAdress().FirstOrDefault(x => x.Url == entity.Url);
            if (response == null)
            {
                context.Entry(entity).State = EntityState.Added;
            }

            context.SaveChanges();
            return entity.Id;
        }
        public void DeleteRSSChanel(UrlRssAdress entity)
        {
            context.UrlRssAdresses.Remove(entity);
            context.SaveChanges();
        }
        public void SetDefaultRssChanel()
        {
            var urlRssAdress = new UrlRssAdress
            {
                Id = new Guid(),
                Url = configuration["RssCHanel:Yandex"]
            };
            SaveUrlRssAdress(urlRssAdress);
            rssService.ParseRssUrl(urlRssAdress);
            urlRssAdress = new UrlRssAdress
            {
                Id = new Guid(),
                Url = configuration["RssCHanel:Mchs"]
            };
            SaveUrlRssAdress(urlRssAdress);
            rssService.ParseRssUrl(urlRssAdress);
        }
    }

}
