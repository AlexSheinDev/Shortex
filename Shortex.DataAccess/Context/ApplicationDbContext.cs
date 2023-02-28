using Microsoft.EntityFrameworkCore;
using Shortex.Common.Models;

namespace Shortex.DataAccess.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ShortUrl> ShortUrls { get; set; }
    }
}
