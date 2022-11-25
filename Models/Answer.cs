using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace quiz_app_backend.Models
{
    public class Answer
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public bool IsCorrect { get; set; }
        public string? questionId { get; set; }
        [ForeignKey("questionId")]
        public Question? Question { get; set; }
    }
}