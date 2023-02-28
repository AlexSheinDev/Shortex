using Shortex.Common.Models;

namespace Shortex.DataAccess.Repositories.IRepositories
{
    /// <summary>
    /// Specific interface logic declaration since in IRepository we declare only generic one.
    /// </summary>
    public interface IShortUrlRepository : IRepository<ShortUrl>
    {
        Task<ShortUrl> GetOriginalUrlByShortUrl(string shortUrl);
        Task<ShortUrl> GetLastUrlByTimeAsync();
        Task ClearShortUrlsAsync();
        Task<bool> ShortLinkExistsAsync(string link);
        Task<bool> LongLinkExistsAsync(string link);
    }
}
