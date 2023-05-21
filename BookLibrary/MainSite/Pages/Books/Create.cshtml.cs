using MainSite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;
using Services.Models;

namespace MainSite.Pages.Books
{
    [Authorize(AuthorizationEnums.ChangeClaim)]
    public class CreateModel : PageModel
    {
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;

        public CreateModel(IAuthorService authorService, IBookService bookService)
        {
            _authorService = authorService;
            _bookService = bookService;
        }

        [BindProperty]
        public BookCreateUpdateVm? Input { get; set; }

        public async Task OnGet()
        {
            Input = new BookCreateUpdateVm
            {
                Authors = await GetAuthorsSelectList()
            };
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                Input.Authors = await GetAuthorsSelectList();
                return Page();
            }
            var book = new RequestBook(Input.Title, Input.Description, Input.Isbn, Input.AuthorId.Value);
            await _bookService.CreateBook(book);
            return RedirectToBooks();
        }
        public IActionResult OnPostCancel()
        {
            return RedirectToBooks();
        }

        private async Task<IEnumerable<SelectListItem>> GetAuthorsSelectList()
        {
            var authors = await _authorService.GetAuthors();
            return authors.Select(x => new SelectListItem(x.Name, x.Id.ToString()));
        }
        private IActionResult RedirectToBooks()
        {
            return RedirectToPage("Index");
        }
    }
}
