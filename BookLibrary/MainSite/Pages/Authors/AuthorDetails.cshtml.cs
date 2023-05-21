using MainSite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace MainSite.Pages
{
    public class AuthorDetailsModel : PageModel
    {
        private readonly IAuthorService _service;

        public AuthorDetailsModel(IAuthorService service)
        {
            _service = service;
        }

        public AuthorDetailedVm? Author { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var author = await _service.GetAuthorDetails(id);
            if (author is null)
            {
                return NotFound();
            }
            var books = author.Books!.Select(x => new BookVm(x.Id, x.Title)).OrderBy(x => x.Title);
            Author = new AuthorDetailedVm(author.Id, author.Name, author.Description, books);
            return Page();
        }
    }
}
