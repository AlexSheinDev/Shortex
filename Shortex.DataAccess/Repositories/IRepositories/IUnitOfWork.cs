namespace Shortex.DataAccess.Repositories.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IShortenedUrlRepository ShortenedUrls { get; }
        Task<int> SaveChangesAsync();
    }
}
