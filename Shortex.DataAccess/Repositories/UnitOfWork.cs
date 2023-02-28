using Microsoft.Extensions.Logging;
using Shortex.DataAccess.Context;
using Shortex.DataAccess.Repositories.IRepositories;

namespace Shortex.DataAccess.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger _logger;
        private bool _disposed;

        private IShortenedUrlRepository _shortenedUrlRepository;

        public UnitOfWork(
            ApplicationDbContext db,
            ILogger logger)
        {
            _db = db;
            _logger = logger;
        }

        public IShortenedUrlRepository ShortenedUrls => _shortenedUrlRepository ??= new ShortenedUrlRepository(_db, _logger);

        public async Task<int> SaveChangesAsync() => await _db.SaveChangesAsync();


        // Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
