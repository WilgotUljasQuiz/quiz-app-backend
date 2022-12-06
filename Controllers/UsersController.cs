using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using quiz_app_backend.Dtos;
using quiz_app_backend.IServices;
using quiz_app_backend.Models;

namespace quiz_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var _login = await _userService.Login(loginDto);
            if (_login.Item2 == true)
            {
                return Ok(_login.Item1);
            }
            else return BadRequest(_login.Item1);
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register(UserDto userDto)
        {
            var _register = await _userService.Register(userDto);
            if (_register.Item2 == true)
            {
                return Ok(_register.Item1);
            }
            else return BadRequest(_register.Item1);
        }

        [HttpDelete("delete"), Authorize]
        public async Task<IActionResult> DeleteUser()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var Id = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var _delete = await _userService.DeleteUser(Id);
            if (_delete.Item2 == true)
            {
                return Ok(_delete.Item1);
            }
            else return BadRequest(_delete.Item1);
        }

        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var _forgotPassword = await _userService.ForgotPassword(forgotPasswordDto);
            if (_forgotPassword.Item2 == true)
            {
                return Ok(_forgotPassword.Item1);
            }
            else return BadRequest(_forgotPassword.Item1);
        }

        [HttpPatch("resetPassword")]
        public async Task<IActionResult> ResetPassword(PasswordResetDto passwordResetDto)
        {
            var _resetPassword = await _userService.ResetPassword(passwordResetDto);
            if (_resetPassword.Item2 == true)
            {
                return Ok(_resetPassword.Item1);
            }
            else return BadRequest(_resetPassword.Item1);
        }

        [HttpGet("getMyStats"), Authorize]
        public Task<StatsDto> GetMyStats()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var Id = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return _userService.GetMyStats(Id);
        }


        [HttpGet("getMyQuizzes"), Authorize]
        public async Task<IEnumerable<Quiz>> GetMyQuizzes()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var Id = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await _userService.GetMyQuizzes(Id);
        }

    }
}