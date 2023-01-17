using MirtekRSSNews.Models;

namespace MirtekRSSNews.Interfaces
{
    public interface IRssService
    {
        public void ParseRssUrl(UrlRssAdress url);
    }
}
