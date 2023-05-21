using MainSite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using Services.Models;

namespace MainSite.Pages.Authors
{
    [Authorize(AuthorizationEnums.ChangeClaim)]
    public class EditModel : PageModel
    {
        private readonly IAuthorService _service;

        public EditModel(IAuthorService service)
        {
            _service = service;
        }

        [BindProperty]
        public AuthorCreateUpdateVm? Input { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var author = await _service.GetAuthor(id);
            if (author is null)
            {
                return NotFound();
            }

            Input = new AuthorCreateUpdateVm
            {
                Name = author.Name,
                Description = author.Description
            };
            return Page();
        }
        public async Task<IActionResult> OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var updatedAuthor = new RequestAuthor(Input.Name, Input.Description);
            await _service.UpdateAuthor(id, updatedAuthor);
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
