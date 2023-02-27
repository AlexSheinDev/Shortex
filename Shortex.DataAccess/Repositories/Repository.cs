using Microsoft.EntityFrameworkCore;
using Shortex.DataAccess.Context;
using Shortex.DataAccess.Repositories.IRepositories;

namespace Shortex.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        public Repository(ApplicationDbContext context)
        {
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll() => _dbSet;

        public T GetById(object id) => _dbSet.Find(id);

        public void Create(T entity) => _dbSet.Add(entity);

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }
    }
}
