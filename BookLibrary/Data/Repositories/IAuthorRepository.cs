using Data.Models;

namespace Data.Repositories
{
    public interface IAuthorRepository : IRepository<DbAuthor>
    {
        Task<DbAuthor?> GetDetailed(int id);
    }
}