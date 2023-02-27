using Shortex.Common.Models;
using Shortex.DataAccess.Context;
using Shortex.DataAccess.Repositories.IRepositories;

namespace Shortex.DataAccess.Repositories
{
    public class ShortenedUrlRepository : Repository<ShortenedUrl>, IShortenedUrlRepository
    {
        public ShortenedUrlRepository(ApplicationDbContext db) : base(db)
        {
            // specific logic implementation based on interface since in Repository we declare only generic one.
        }
    }
}
