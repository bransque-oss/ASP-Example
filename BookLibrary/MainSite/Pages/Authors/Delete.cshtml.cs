using MainSite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace MainSite.Pages.Authors
{
    [Authorize(AuthorizationEnums.ChangeClaim)]
    public class DeleteModel : PageModel
    {
        private readonly IAuthorService _service;

        public DeleteModel(IAuthorService service)
        {
            _service = service;
        }

        public AuthorDeleteVm? Author { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var dbAuthor = await _service.GetAuthor(id);
            if (dbAuthor is null)
            {
                return NotFound();
            }
            Author = new AuthorDeleteVm(dbAuthor.Name);
            return Page();
        }
        public async Task<IActionResult> OnPost(int id)
        {
            await _service.DeleteAuthor(id);
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
