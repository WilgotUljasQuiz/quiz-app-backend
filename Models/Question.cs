using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace quiz_app_backend.Models
{
    public class Question
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public string? QuizId { get; set; }
        [ForeignKey("QuizId")]
        public Quiz? Quiz { get; set; }
        public ICollection<Answer> Answers { get; set; }

    }
}