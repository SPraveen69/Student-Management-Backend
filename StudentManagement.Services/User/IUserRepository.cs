using StudentManagement.Models;
using StudentManagement.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Services.User
{
    public interface IUserRepository
    {
        public StudentManagement.Models.User Adduser(UserDto userDto);
    }
}
