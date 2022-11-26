using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace quiz_app_backend.Models
{
    public class Game
    {
        [Key]
        public string Id { get; set; }
        public string? QuizId { get; set; }
        [ForeignKey("QuizId")]
        public Quiz? Quiz { get; set; }

        public string? UserID { get; set; }
        [ForeignKey("UserID")]
        public User? User { get; set; }
        public ICollection<Score> Scores { get; set; }
    }
}
