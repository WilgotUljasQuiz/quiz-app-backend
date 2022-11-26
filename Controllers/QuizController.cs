using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using quiz_app_backend.IServices;
using quiz_app_backend.Models;
using quiz_app_backend.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
        public async Task<IEnumerable<Question>> GetQuestions(SendStringDto getByIdDto)
        {
           return await _quizService.GetQuestions(getByIdDto);
        }

        [HttpGet("getAnswers")]
        public async Task<IEnumerable<Answer>> GetAnswers(SendStringDto getByIdDto)
        {
           return await _quizService.GetAnswers(getByIdDto);
        }

        [HttpPost("createQuiz"), Authorize]
        public async Task<IEnumerable<Answer>> CreateQuiz(SendStringDto stringDto, string UserId)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var Id = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await _quizService.GetAnswers(stringDto);
        }

        [HttpPost("createQuestion"), Authorize]
        public async Task<string> CreateQuestion(CreateQuestionDto createQuestionDto)
        {
            return _quizService.CreateQuestion(createQuestionDto);
        }

    }
}