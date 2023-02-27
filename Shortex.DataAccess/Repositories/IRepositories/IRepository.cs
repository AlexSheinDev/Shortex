namespace Shortex.DataAccess.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Create(T entity);
        void Update(T entity);
        void Delete(object id);
    }
}
