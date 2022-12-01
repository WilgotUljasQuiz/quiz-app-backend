using quiz_app_backend.Dtos;

namespace quiz_app_backend.IServices
{
    public interface IUserService
    {
        Task<(string, bool)> Register(UserDto user);
        Task<(string, bool)> Login(LoginDto loginDto);
        Task<(string, bool)> DeleteUser(string Id);
        Task<(string, bool)> ForgotPassword(ForgotPasswordDto forgotPasswordDto);
        Task<(string, bool)> ResetPassword(PasswordResetDto resetPasswordDto);

    }
}
