using Data;
using Data.Models;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.Exceptions;
using Services.Models;

namespace Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;

        public AuthorService(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ResponseAuthor>> GetAuthors()
        {
            var dbAuthors = await _repository.GetAll();
            return dbAuthors.Select(x => new ResponseAuthor(x.Id, x.Name, x.Description, new List<ResponseBook>()))
                .ToList();
        }
        public async Task<ResponseAuthor?> GetAuthor(int id)
        {
            var dbAuthor = await _repository.Get(id);
            if (dbAuthor is null)
            {
                throw new ForUserException($"There is no such author with id '{id}'.");
            }
            return new ResponseAuthor(dbAuthor.Id, dbAuthor.Name, dbAuthor.Description, null);
        }
        public async Task<ResponseAuthor?> GetAuthorDetails(int id)
        {
            var dbAuthor = await _repository.GetDetailed(id);
            if (dbAuthor is null)
            {
                throw new ForUserException($"There is no such author with id '{id}'.");
            }
            var books = dbAuthor.Books.Select(x => new ResponseBook(x.Id, x.Title, x.Description, x.Isbn, x.AuthorId));
            return new ResponseAuthor(dbAuthor.Id, dbAuthor.Name, dbAuthor.Description, books);
        }
        public async Task CreateAuthor(RequestAuthor newAuthor)
        {
            var dbAuthor = new DbAuthor
            {
                Name = newAuthor.Name,
                Description = newAuthor.Description
            };
            await _repository.Add(dbAuthor);
        }
        public async Task UpdateAuthor(int id, RequestAuthor updatedAuthor)
        {
            if (!await _repository.IsExist(id))
            {
                throw new ForUserException($"Unable to update author with id '{id}' because it doesn't exist.");
            }
            var dbAuthor = new DbAuthor
            {
                Id = id,
                Name = updatedAuthor.Name,
                Description = updatedAuthor.Description
            };
            await _repository.Update(dbAuthor);
        }
        public async Task DeleteAuthor(int id)
        {
            if (!await _repository.IsExist(id))
            {
                throw new ForUserException($"Unable to delete author with id '{id}' because it doesn't exist.");
            }
            await _repository.Delete(id);
        }
    }
}
