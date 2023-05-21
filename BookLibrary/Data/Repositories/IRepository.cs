using Data.Models;

namespace Data.Repositories
{
    public interface IRepository<T>
    {
        Task Add(T entity);
        Task Delete(int id);
        Task<T?> Get(int id);
        Task<IEnumerable<T>> GetAll();
        Task<bool> IsExist(int id);
        Task Update(T entity);
    }
}