using StudentManagement.DataAccess;
using StudentManagement.Models;
using StudentManagement.Services.Models;
using StudentManagement.Services.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Services.User
{
    public class UserService : IUserRepository
    {
        private readonly SMDbContext _context = new SMDbContext();
        StudentManagement.Models.User IUserRepository.Adduser(UserDto userDto)
        {
            if (_context.Users.Any(u => u.Username == userDto.Username)) 
            { 
                throw new Exception("Username already exists");
            }
            string hashpassword = JWTManager.HashPassword(userDto.Password);

            var user = new StudentManagement.Models.User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Username = userDto.Username,
                Password = hashpassword,
                Status = Status.Active,
                last_sync_date_time = DateTime.Now,
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
    }
}
