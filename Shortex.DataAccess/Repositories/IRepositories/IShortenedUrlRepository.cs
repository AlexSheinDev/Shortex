using Shortex.Common.Models;

namespace Shortex.DataAccess.Repositories.IRepositories
{
    public interface IShortenedUrlRepository : IRepository<ShortenedUrl>
    {
        // specific interface logic declaration since in IRepository we declare only generic one.
    }
}
