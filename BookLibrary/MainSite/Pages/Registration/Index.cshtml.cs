using System.Security.Claims;
using MainSite.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace MainSite.Pages.Registration
{
    public class IndexModel : PageModel
    {
        private readonly IAuthorizationService _service;

        public IndexModel(IAuthorizationService service)
        {
            _service = service;
        }

        [BindProperty]
        public UserVm? UserInput { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (await _service.IsExist(UserInput.Login))
            {
                ModelState.AddModelError(string.Empty, "User with this login already exists.");
                return Page();
            }

            await _service.AddUser(UserInput.Login, UserInput.Password);
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Index");
        }
    }
}
