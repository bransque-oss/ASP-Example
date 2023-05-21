using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MainSite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<BookDetailsModel> _logger;

        public IndexModel(ILogger<BookDetailsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}