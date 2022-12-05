using System.ComponentModel.DataAnnotations;

namespace quiz_app_backend.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAccountAt { get; set; }
        public ICollection<Quiz> Quizzes { get; set; }
        public ICollection<Game> Games { get; set; }

        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpirationDate { get; set; }
        public string? PasswordHash { get; set; }
    }
}
