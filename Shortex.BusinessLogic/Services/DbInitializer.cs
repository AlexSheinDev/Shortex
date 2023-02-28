using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shortex.BusinessLogic.Services.IServices;
using Shortex.DataAccess.Context;

namespace Shortex.BusinessLogic.Services
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger _logger;

        public DbInitializer(
            ApplicationDbContext db,
            ILogger<DbInitializer> logger)
        {
            _db = db;
            _logger = logger;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Any())
                {
                    _db.Database.Migrate();
                    _logger.LogInformation($"Database was initialized at {DateTime.UtcNow}.");
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"While initializing database, error was thrown: {ex.ToString()}.");
                throw;
            }
        }
    }
}
