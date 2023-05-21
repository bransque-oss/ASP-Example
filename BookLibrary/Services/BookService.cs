using Data;
using Data.Models;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.Exceptions;
using Services.Models;

namespace Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<DbBook> _repository;

        public BookService(IRepository<DbBook> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ResponseBook>> GetBooks()
        {
            var dbBooks = await _repository.GetAll();
            return dbBooks.Select(x => new ResponseBook(x.Id, x.Title, x.Description, x.Isbn, x.AuthorId));
        }
        public async Task<ResponseBook?> GetBook(int id)
        {
            var dbBook = await _repository.Get(id);    
            if (dbBook is null)
            {
                throw new ForUserException($"There is no such book with id '{id}'.");
            }
            else
            {
                return new ResponseBook(dbBook.Id, dbBook.Title, dbBook.Description, dbBook.Isbn, dbBook.AuthorId);
            }
        }
        public async Task CreateBook(RequestBook newBook)
        {
            var dbBook = new DbBook
            {
                Title = newBook.Title,
                Description = newBook.Description,
                Isbn = newBook.Isbn,
                AuthorId = newBook.AuthorId
            };
            await _repository.Add(dbBook);
        }
        public async Task UpdateBook(int id, RequestBook updatedBook)
        {
            if (!await _repository.IsExist(id))
            {
                throw new ForUserException($"Unable to update book with id '{id}' because it doesn't exist.");
            }
            var dbBook = new DbBook
            {
                Id = id,
                Title = updatedBook.Title,
                Description = updatedBook.Description,
                Isbn = updatedBook.Isbn,
                AuthorId = updatedBook.AuthorId
            };
            await _repository.Update(dbBook);
        }
        public async Task DeleteBook(int id)
        {
            if (!await _repository.IsExist(id))
            {
                throw new ForUserException($"Unable to delete book with id '{id}' because it doesn't exist.");
            }
            await _repository.Delete(id);
        }
    }
}
