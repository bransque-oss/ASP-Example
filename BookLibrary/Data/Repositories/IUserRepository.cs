using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public interface IUserRepository
    {
        Task Add(DbUser entity);
        Task<DbUser?> Get(string login, string password);
        Task<bool> IsExist(string login);
    }
}