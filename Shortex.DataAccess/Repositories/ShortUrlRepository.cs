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
    public class ShortUrlRepository : Repository<ShortUrl>, IShortUrlRepository
    {
        private readonly DbSet<ShortUrl> _dbSet;
        private readonly ILogger _logger;

        public ShortUrlRepository(
            ApplicationDbContext db,
            ILoggerFactory logFactory) : base(db)
        {
            _dbSet = db.Set<ShortUrl>();
            _logger = logFactory.CreateLogger<ShortUrlRepository>();
        }

        public async Task<ShortUrl> GetOriginalUrlByShortUrl(string shortUrl)
        {
            var allShortUrls = _dbSet;
            if (await allShortUrls.AnyAsync())
            {
                var shortUrlFromDb = await allShortUrls.FirstOrDefaultAsync(f => f.ShortenedUrl == shortUrl);

                _logger.LogInformation($"Retrieved the shortened Url: {shortUrl}.");

                return shortUrlFromDb;
            }

            throw new Exception("There are no any saved shortened Urls.");
        }

        public async Task<ShortUrl> GetLastUrlByTimeAsync()
        {
            var allShortUrls = _dbSet;
            if (await allShortUrls.AnyAsync())
            {
                var lastUrl = await allShortUrls
                                    .OrderByDescending(od => od.CreatedAt)
                                    .FirstAsync();

                _logger.LogInformation($"Retrieved the last saved shortened Url: {lastUrl.ShortenedUrl} with creation time {lastUrl.CreatedAt}.");

                return lastUrl;
            }

            throw new Exception("There are no any saved shortened Urls.");
        }

        public async Task ClearShortUrlsAsync()
        {
            var allShortUrls = _dbSet;
            if (await allShortUrls.AnyAsync())
            {
                _dbSet.RemoveRange(allShortUrls);
            }
        }

        public async Task<bool> ShortLinkExistsAsync(string link) => await _dbSet.AnyAsync(a => a.ShortenedUrl == link);
        public async Task<bool> LongLinkExistsAsync(string link) => await _dbSet.AnyAsync(a => a.LongUrl == link);
    }
}
