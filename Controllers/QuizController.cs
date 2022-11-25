using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using quiz_app_backend.IServices;
using quiz_app_backend.Models;
using quiz_app_backend.Dtos;

namespace quiz_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet("getQuizzes")]
        public async Task<IEnumerable<Quiz>> GetQuizzes()
        {
           return await _quizService.GetQuizzes();
        }

        [HttpGet("getQuestions")]
        public async Task<IEnumerable<Question>> GetQuestions(GetByIdDto getByIdDto)
        {
           return await _quizService.GetQuestions(getByIdDto);
        }

        [HttpGet("getAnswers")]
        public async Task<IEnumerable<Answer>> GetAnswers(GetByIdDto getByIdDto)
        {
           return await _quizService.GetAnswers(getByIdDto);
        }

    }
}