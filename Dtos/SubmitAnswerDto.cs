using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quiz_app_backend.Dtos
{
    public class SubmitAnswerDto
    {
        public string GameId { get; set; }
        public string AnswerId { get; set; }

    } 

}