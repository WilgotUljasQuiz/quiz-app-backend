
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using quiz_app_backend.IServices;
using quiz_app_backend.Dtos;
using quiz_app_backend.Models;
using Microsoft.AspNetCore.Identity;
namespace quiz_app_backend.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;
        private readonly IMailService _mailService;


        public UserService(IConfiguration configuration, DataContext context, IMailService mailService)
        {
            _configuration = configuration;
            _context = context;
            _mailService = mailService;
        }
        public async Task<(string, bool)> Login(LoginDto loginDto)
        {
            var data = _context.Users;
            var query = from user in data
                        where user.Username == loginDto.Username
                        select user;

            User _user = (query.First());
            if (query.Count() > 0)
            {
                try
                {
                    if (Identify(loginDto.Password, _user))
                    {
                        return (CreateJWT(_user), true);
                    }
                    else return ("Wrong password", false);
                }
                catch (Exception e)
                {
                    return (e.Message, false);
                }

            }
            else
            {
                return ("User not found", false);
            }


        }

        public async Task<(string, bool)> Register(UserDto userDto)
        {
            if (!UserExists(userDto.Username, userDto.Email))
            {
                string _role;

                if (userDto.Username == "admin")
                    _role = "admin";
                else
                    _role = "user";

                string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
                User user = new User()
                {
                    Username = userDto.Username,
                    Email = userDto.Email,
                    Role = _role,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password, salt),
                    Id = CreateToken("user")
                };


                _context.Add(user);
                await _context.SaveChangesAsync();
                return ("User registred", true);
            }
            else
            {
                return ("User exists", false);
            }

        }
        public async Task<(string, bool)> DeleteUser(string Id)
        {
            var users = await _context.Users.FindAsync(Id);
            if (users != null)
            {
                _context.Remove(users);
                await _context.SaveChangesAsync();
                return ("User deleted", true);
            }
            else return ("User not found", false);
        }
        public async Task<(string, bool)> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            string result;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == forgotPasswordDto.Email);

            if (user != null)
            {
                try
                {
                    string token = CreateToken("resettoken");
                    user.ResetToken = token;
                    user.ResetTokenExpirationDate = DateTime.Now.AddDays(1);
                    await _context.SaveChangesAsync();

                    result = _mailService.SendEmail(new MailDto() { To = forgotPasswordDto.Email, Body = token });
                }
                catch (Exception e)
                {
                    return (e.ToString(), false);
                }

                return (result, true);
            }
            else
            {
                return ("User does not exist", false);
            }
        }

        public async Task<(string, bool)> ResetPassword(PasswordResetDto passwordResetDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.ResetToken == passwordResetDto.Token);

            if (user != null || user.ResetTokenExpirationDate > DateTime.Now)
            {
                string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordResetDto.NewPassword, salt);
                user.ResetToken = null;
                user.ResetTokenExpirationDate = null;
                await _context.SaveChangesAsync();

                return ("User password has been reset", true);
            }
            else return ("User does not exist", false);
        }


        private bool Identify(string password, User user)
        {
            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }

        private string CreateJWT(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private bool UserExists(string Username, string Email)
        {
            bool userName = (_context.Users?.Any(e => e.Username == Username)).GetValueOrDefault();
            bool email = (_context.Users?.Any(e => e.Email == Email)).GetValueOrDefault();
            if (userName == false && email == false) return false;
            else
            {
                return true;
            }

        }
        private string CreateToken(string type)
        {
            string token = "";
            bool exists = true;

            while (exists)
            {
                token = Nanoid.Nanoid.Generate();

                switch (type)
                {
                    case "resettoken":
                        if ((_context.Users?.Any(e => e.ResetToken == token)).GetValueOrDefault())
                            exists = true;
                        else exists = false;
                    break;

                    case "user":
                        if ((_context.Users?.Any(e => e.Id == token)).GetValueOrDefault())
                            exists = true;
                        else exists = false;
                    break;
                }
            }
            return token;
        }

    }
}