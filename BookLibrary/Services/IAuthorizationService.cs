using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Models;

namespace Services
{
    public interface IAuthorizationService
    {
        Task AddUser(string login, string password);
        Task<UserResponse?> GetUser(string login, string password);
        Task<string> CreateJwtToken(string login, string password);
        Task<bool> IsExist(string login);
    }
}
