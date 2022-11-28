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
        public IEnumerable<Quiz> GetQuizzes()
        {
           return _quizService.GetQuizzes();
        }

        [HttpGet("getQuestionIds")]
        public IEnumerable<string> GetQuestionIds(string quizId)
        {
           return _quizService.GetQuestionIds(quizId);
        }

        [HttpGet("getQuestion")]
        public Question GetQuestion(string questionId)
        {
           return _quizService.GetQuestion(questionId);
        }


        [HttpPost("createQuiz"), Authorize]
        public async Task<string> CreateQuiz(CreateQuizDto createQuizDto)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var Id = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return _quizService.CreateQuiz(createQuizDto, Id);
        }

        [HttpPost("createQuestion"), Authorize]
        public async Task<string> CreateQuestion(CreateQuestionDto createQuestionDto)
        {
            return _quizService.CreateQuestion(createQuestionDto);
        }

        [HttpPost("createGame"), Authorize]
        public async Task<string> CreateGame(string QuizId)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var Id = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return _quizService.CreateGame(QuizId, Id);
        }
    }
}