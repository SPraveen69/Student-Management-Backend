using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudentManagement.DataAccess;
using StudentManagement.Services.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Services.Token
{
    public class JWTManager : IJWTRepository
    {
        private readonly IConfiguration _configuration;
        private readonly SMDbContext _context;
        private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        private static readonly int SaltSize = 16;
        private static readonly int HashSize = 20;
        private static readonly int Iterations = 10000;

        public JWTManager(IConfiguration configuration, SMDbContext context)
        { 
            this._configuration = configuration;
            this._context = context;        
        }

        StudentManagement.Models.Token IJWTRepository.Authenticate(UserLoginDto userLoginDto)
        {
            try
            {
                var userFromDb = _context.Users.FirstOrDefault(a => a.Username == userLoginDto.Username);

                if (userFromDb == null)
                {
                    return null;
                }

                if (VerifyPassword(userFromDb.Password, userLoginDto.Password))
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenKey = Encoding.UTF8.GetBytes("secret key");

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new System.Security.Claims.ClaimsIdentity(
                            new Claim[]
                            {
                                new Claim(ClaimTypes.Name, userLoginDto.Username)
                            }),
                        Expires = DateTime.UtcNow.AddMinutes(5),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    return new StudentManagement.Models.Token
                    {
                        TokenId = tokenHandler.WriteToken(token)
                    };
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred during authentication: {ex.Message}");
                return null;
            }
        }

        public static string HashPassword(string password)
        {

            byte[] salt;
            rng.GetBytes(salt = new byte[SaltSize]);
            var key = new Rfc2898DeriveBytes(password, salt, Iterations);
            var hash = key.GetBytes(HashSize);

            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            var base64Hash = Convert.ToBase64String(hashBytes);
            return base64Hash;
        }

        public static bool VerifyPassword(string base64Hash, string password)
        {
            var hashBytes = Convert.FromBase64String(base64Hash);

            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            var key = new Rfc2898DeriveBytes(password, salt, Iterations);
            byte[] hash = key.GetBytes(HashSize);

            for (var i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
