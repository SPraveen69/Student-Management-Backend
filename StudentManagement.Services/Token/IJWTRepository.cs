using StudentManagement.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Services.Token
{
    public interface IJWTRepository
    {
        StudentManagement.Models.Token Authenticate(UserLoginDto userLoginDto); 
    }
}
