using Shortex.DataAccess.Context;
using Shortex.DataAccess.Repositories.IRepositories;

namespace Shortex.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private bool _disposed;

        private IShortenedUrlRepository _shortenedUrlRepository;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
        }

        public IShortenedUrlRepository ShortenedUrls => _shortenedUrlRepository ??= new ShortenedUrlRepository(_db);

        public async Task<int> SaveChangesAsync() => await _db.SaveChangesAsync();


        // Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
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
