using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Services.Models;
using StudentManagement.Services.Token;
using StudentManagement.Services.User;

namespace StudentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJWTRepository _jwtRepository;
        private readonly IUserRepository _userRepository;

        public UserController(IJWTRepository jwtRepository, IUserRepository userRepositoy)
        {
            this._jwtRepository = jwtRepository;
            this._userRepository = userRepositoy;
        }

        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(UserLoginDto userLoginDto)
        {
            var token = _jwtRepository.Authenticate(userLoginDto);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [HttpPost]
        [Route("register")]
        public IActionResult register(UserDto userDto)
        {
            var user = _userRepository.Adduser(userDto);
            return Ok(user);
        }
    }
}
