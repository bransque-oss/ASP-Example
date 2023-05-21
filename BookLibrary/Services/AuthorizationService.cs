using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Data.Repositories;
using Services.Exceptions;
using Services.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserRepository _repository;

        public AuthorizationService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task AddUser(string login, string password)
        {
            var encryptedPassword = EncryptPassword(password);
            var user = new DbUser
            {
                Login = login,
                Password = encryptedPassword,
                CanChangeEntities = false
            };
            await _repository.Add(user);
        }

        public async Task<string> CreateJwtToken(string login, string password)
        {
            var dbUser = await GetUser(login, password);
            if (dbUser != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, "TokenForWebApi"),
                    new Claim(JwtRegisteredClaimNames.Iat, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, DateTime.Now.ToString()),
                    new Claim(AuthorizationEnums.LoginClaimName, dbUser.Login),
                    new Claim(AuthorizationEnums.ChangeClaim, dbUser.CanChangeEntities.ToString())
                };

                var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bookLibraryApiSecretKey")), SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    "bookLibraryWebApi",
                    "bookLibraryWebApi",
                    claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: credentials);

                var tokenHandler = new JwtSecurityTokenHandler();
                return tokenHandler.WriteToken(token);
            }

            return string.Empty;
        }

        public async Task<UserResponse?> GetUser(string login, string password)
        {
            var encryptedPassword = EncryptPassword(password);
            var dbUser = await _repository.Get(login, encryptedPassword);
            if (dbUser == null)
            {
                throw new ForUserException("User with such login or password is not exist.");
            }
            return new UserResponse(dbUser.Id, dbUser.Login, dbUser.CanChangeEntities);
        }

        public async Task<bool> IsExist(string login)
        {
            return await _repository.IsExist(login);
        }

        private string EncryptPassword(string password)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            using (var sha512 = SHA512.Create())
            {
                var hash = sha512.ComputeHash(passwordBytes);
                var encryptedPassword = BitConverter.ToString(hash).Replace("-", "");
                return encryptedPassword;
            }
        }
    }
}
