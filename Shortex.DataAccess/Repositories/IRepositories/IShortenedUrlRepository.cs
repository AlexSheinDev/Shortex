using Shortex.Common.Models;

namespace Shortex.DataAccess.Repositories.IRepositories
{
    /// <summary>
    /// Specific interface logic declaration since in IRepository we declare only generic one.
    /// </summary>
    public interface IShortenedUrlRepository : IRepository<ShortenedUrl>
    {

        Task<ShortenedUrl> GetLastUrlByTimeAsync();
        Task ClearShortenedUrlsAsync();
    }
}
