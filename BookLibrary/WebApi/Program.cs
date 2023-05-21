using System.Text;
using Data;
using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<BookLibraryDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("BookLibraryDatabase"));
            });
            builder.Services.AddTransient<CommonExceptionHandleMiddleware>();
            builder.Services.AddScoped<Services.IAuthorizationService, AuthorizationService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IRepository<DbBook>, BookRepository>();
            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IBookService, BookService>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "bookLibraryWebApi",
                        ValidAudience = "bookLibraryWebApi",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bookLibraryApiSecretKey"))
                    };
                });
            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.AddPolicy(AuthorizationEnums.ChangeClaim, policy => policy.RequireClaim(AuthorizationEnums.ChangeClaim, "True"));
            });

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseMiddleware<CommonExceptionHandleMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.Run();
        }
    }
}