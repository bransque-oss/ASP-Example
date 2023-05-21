using Services.Models;

namespace Services
{
    public interface IBookService
    {
        Task<IEnumerable<ResponseBook>> GetBooks();
        Task<ResponseBook?> GetBook(int id);
        Task CreateBook(RequestBook newBook);
        Task UpdateBook(int id, RequestBook updatedBook);
        Task DeleteBook(int id);
    }
}
