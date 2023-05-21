using MainSite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace MainSite.Pages.Books
{
    [Authorize(AuthorizationEnums.ChangeClaim)]
    public class DeleteModel : PageModel
    {
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;

        public DeleteModel(IAuthorService authorService, IBookService bookService)
        {
            _authorService = authorService;
            _bookService = bookService;
        }

        public BookDeleteVm? Book { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var dbBook = await _bookService.GetBook(id);
            if (dbBook is null)
            {
                return NotFound();
            }
            var author = await _authorService.GetAuthor(dbBook.AuthorId);
            if (author is null)
            {
                return NotFound();
            }
            Book = new BookDeleteVm(dbBook.Id, dbBook.Title, author.Name);
            return Page();
        }
        public async Task<IActionResult> OnPost(int id)
        {
            await _bookService.DeleteBook(id);
            return RedirectToBooks();
        }
        public IActionResult OnPostCancel()
        {
            return RedirectToBooks();
        }

        private IActionResult RedirectToBooks()
        {
            return RedirectToPage("Index");
        }
    }
}
