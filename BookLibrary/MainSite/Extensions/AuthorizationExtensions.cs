using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Services;

namespace MainSite.Extensions
{
    public static class AuthorizationExtensions
    {
        public static WebApplicationBuilder AddCookieAuthorizaion(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationEnums.ChangeClaim, policy => policy.RequireClaim(AuthorizationEnums.ChangeClaim, "True"));
            });
            return builder;

        }
    }
}
