using quiz_app_backend.Dtos;

namespace quiz_app_backend.Services
{
    public interface IMailService
    {
        public string SendEmail(MailDto emailDto);
    }
}
