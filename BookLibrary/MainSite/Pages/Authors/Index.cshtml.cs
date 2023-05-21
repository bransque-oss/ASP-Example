using MainSite.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace MainSite.Pages.Authors
{
    public class IndexModel : PageModel
    {
        private readonly IAuthorService _service;

        public IndexModel(IAuthorService service)
        {
            _service = service;
        }

        public IEnumerable<AuthorVm> Authors { get; set; } = new List<AuthorVm>();

        public async Task OnGet()
        {
            var authors = await _service.GetAuthors();
            Authors = authors.Select(x => new AuthorVm(x.Id, x.Name))
                .OrderBy(x => x.Name);
        }
    }
}
