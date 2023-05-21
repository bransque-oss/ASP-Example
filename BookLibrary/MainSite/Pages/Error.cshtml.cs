using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Services.Exceptions;

namespace MainSite.Pages
{
    public class ErrorModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            HandleExceptionMessage();
        }

        public void OnPost()
        {
            HandleExceptionMessage();
        }
        public IActionResult OnGetToMainPage()
        {
            return RedirectToPage("/Index");
        }

        private void HandleExceptionMessage()
        {
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (exceptionHandlerFeature.Error is ForUserException ex)
            {
                Message = ex.Message;
            }
            else
            {
                Message = "Something went wrong.";
            }
        }
    }
}