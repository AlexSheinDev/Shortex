using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shortex.Common.Models;
using Shortex.DataAccess.Context;
using Shortex.DataAccess.Repositories.IRepositories;

namespace Shortex.DataAccess.Repositories
{
    /// <summary>
    /// Specific logic implementation based on interface since in Repository we declare only generic one.
    /// </summary>
    public class ShortenedUrlRepository : Repository<ShortenedUrl>, IShortenedUrlRepository
    {
        private readonly DbSet<ShortenedUrl> _dbSet;
        private readonly ILogger _logger;
        public ShortenedUrlRepository(
            ApplicationDbContext db,
            ILogger logger) : base(db)
        {
            _dbSet = db.Set<ShortenedUrl>();
            _logger = logger;
        }

        public async Task<ShortenedUrl> GetLastUrlByTimeAsync()
        {
            var allShortenedUrls = _dbSet;
            if (await allShortenedUrls.AnyAsync())
            {
                var lastUrl = await allShortenedUrls
                                    .OrderByDescending(od => od.CreatedAt)
                                    .FirstAsync();

                _logger.LogInformation($"Retrieved the last saved shortened Url: {lastUrl.ShortUrl} with creation time {lastUrl.CreatedAt}.");

                return lastUrl;
            }

            throw new Exception("There are no any saved shortened Urls.");
        }

        public async Task ClearShortenedUrlsAsync()
        {
            var allShortenedUrls = _dbSet;
            if (await allShortenedUrls.AnyAsync())
            {
                _dbSet.RemoveRange(allShortenedUrls);
            }
        }
    }
}
