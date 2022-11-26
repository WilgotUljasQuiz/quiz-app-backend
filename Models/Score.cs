using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace quiz_app_backend.Models
{
    public class Score
    {
        [Key]
        public int Id { get; set; }
        public bool AnswerCorrect { get; set; }
        public string? GameId { get; set; }
        [ForeignKey("GameId")]
        public Game? Game { get; set; }
    }
}
