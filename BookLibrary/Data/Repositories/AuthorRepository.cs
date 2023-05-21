using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookLibraryDbContext _context;

        public AuthorRepository(BookLibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DbAuthor>> GetAll()
        {
            return await _context.Authors.ToListAsync();
        }
        public async Task<DbAuthor?> Get(int id)
        {
            return await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<DbAuthor?> GetDetailed(int id)
        {
            return await _context.Authors.Include(x => x.Books)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<bool> IsExist(int id)
        {
            var dbAuthor = await _context.Authors.FindAsync(id);
            return dbAuthor != null;
        }
        public async Task Add(DbAuthor author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }
        public async Task Update(DbAuthor author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var dbAuthor = new DbAuthor { Id = id };
            _context.Authors.Remove(dbAuthor);
            await _context.SaveChangesAsync();
        }
    }
}
