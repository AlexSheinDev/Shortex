namespace Shortex.DataAccess.Repositories.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IShortUrlRepository ShortUrls { get; }
        Task<int> SaveChangesAsync();
    }
}
