using System.Threading.Tasks;
using MirtekRSSNews.Models;

namespace MirtekRSSNews.Interfaces
{
    public interface IRssChanelsService
    {
        public Task ParseRssUrl(RSSUrl url);
        public void SetDefaultRssChanel();

    }
}
