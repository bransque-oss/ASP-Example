using System.Security.Claims;
using MainSite.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace MainSite.Pages.Login
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

            var user = await _service.GetUser(UserInput.Login, UserInput.Password);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User login or password is wrong.");
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(AuthorizationEnums.LoginClaimName, user.Login),
                new Claim(AuthorizationEnums.ChangeClaim, user.CanChangeEntities.ToString())
            };
            var claimIdentity = new ClaimsIdentity(claims, AuthorizationEnums.AuthType);
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = false,
                AllowRefresh = false
            });
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Index");
        }
    }
}
