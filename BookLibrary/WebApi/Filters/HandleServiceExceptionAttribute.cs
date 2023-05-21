using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.Exceptions;

namespace WebApi.Filters
{
    public class HandleServiceExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ForUserException ex)
            {
                context.Result = new ObjectResult(ex.Message)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
                context.ExceptionHandled = true;
                return;
            }
        }
    }
}
