namespace quiz_app_backend.Dtos
{
    public class PasswordResetDto
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }

    }
}