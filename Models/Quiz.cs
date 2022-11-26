using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace quiz_app_backend.Models
{
    public class Quiz
    {
        [Key]
        public string Id { get; set; }
        public string QuizName { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<Game> Games { get; set; }

    }
}
