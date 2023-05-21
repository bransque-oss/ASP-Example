using Services.Models;

namespace Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<ResponseAuthor>> GetAuthors();
        Task<ResponseAuthor?> GetAuthor(int id);
        Task<ResponseAuthor?> GetAuthorDetails(int id);
        Task CreateAuthor(RequestAuthor newAuthor);
        Task UpdateAuthor(int id, RequestAuthor updatedAuthor);
        Task DeleteAuthor(int id);
    }
}
