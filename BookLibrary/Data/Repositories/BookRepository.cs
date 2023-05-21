using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class BookRepository : IRepository<DbBook>
    {
        private readonly BookLibraryDbContext _context;

        public BookRepository(BookLibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DbBook>> GetAll()
        {
            return await _context.Books.ToListAsync();
        }
        public async Task<DbBook?> Get(int id)
        {
            return await _context.Books.Include(x => x.Author)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<bool> IsExist(int id)
        {
            var dbBook = await _context.Books.FindAsync(id);
            return dbBook != null;
        }
        public async Task Add(DbBook book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }
        public async Task Update(DbBook book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var dbBook = new DbBook { Id = id };
            _context.Books.Remove(dbBook);
            await _context.SaveChangesAsync();
        }
    }
}
