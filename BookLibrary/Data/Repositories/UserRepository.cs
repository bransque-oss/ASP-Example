using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BookLibraryDbContext _context;

        public UserRepository(BookLibraryDbContext context)
        {
            _context = context;
        }

        public async Task Add(DbUser entity)
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<DbUser> Get(string login, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login ==  login && x.Password == password);
            return user;
        }

        public async Task<bool> IsExist(string login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == login);
            return user != null;
        }
    }
}
