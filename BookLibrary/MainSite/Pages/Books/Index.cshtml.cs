using MainSite.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace MainSite.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly IBookService _service;

        public IndexModel(IBookService service)
        {
            _service = service;
        }

        public IEnumerable<BookVm> Books { get; set; } = new List<BookVm>();

        public async Task OnGet()
        {
            var books = await _service.GetBooks();
            Books = books.Select(x => new BookVm(x.Id, x.Title))
                .OrderBy(x => x.Title);
        }
    }
}
