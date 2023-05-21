using MainSite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using Services.Models;

namespace MainSite.Pages.Authors
{
    [Authorize(AuthorizationEnums.ChangeClaim)]
    public class CreateModel : PageModel
    {
        private readonly IAuthorService _service;

        public CreateModel(IAuthorService service)
        {
            _service = service;
        }

        [BindProperty]
        public AuthorCreateUpdateVm? Input { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var author = new RequestAuthor(Input.Name, Input.Description);
            await _service.CreateAuthor(author);
            return RedirectToAuthors();
        }
        public IActionResult OnPostCancel()
        {
            return RedirectToAuthors();
        }

        private IActionResult RedirectToAuthors()
        {
            return RedirectToPage("Index");
        }
    }
}
