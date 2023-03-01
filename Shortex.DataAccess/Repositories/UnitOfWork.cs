using Microsoft.Extensions.Logging;
using Shortex.DataAccess.Context;
using Shortex.DataAccess.Repositories.IRepositories;

namespace Shortex.DataAccess.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private readonly ILoggerFactory _loggerFactory;
        private bool _disposed;

        private IShortUrlRepository _shortUrlRepository;

        public UnitOfWork(
            ApplicationDbContext db,
            ILoggerFactory loggerFactory)
        {
            _db = db;
            _loggerFactory = loggerFactory;
        }

        public IShortUrlRepository ShortUrls => _shortUrlRepository ??= new ShortUrlRepository(_db, _loggerFactory);

        public async Task<int> SaveChangesAsync() => await _db.SaveChangesAsync();


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
