using MainSite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace MainSite.Pages
{
    public class BookDetailsModel : PageModel
    {
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;

        public BookDetailsModel(IAuthorService authorService, IBookService bookService)
        {
            _authorService = authorService;
            _bookService = bookService;
        }

        public BookDetailedVm? Book { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var book = await _bookService.GetBook(id);
            if (book is null)
            {
                return NotFound();
            }
            var author = await _authorService.GetAuthor(book.AuthorId);
            if (author is null)
            {
                return NotFound();
            }

            Book = new BookDetailedVm(book.Id, book.Title, book.Description, book.Isbn, author.Name);
            return Page();
        }
    }
}
