using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using quiz_app_backend.Models;
using quiz_app_backend.Dtos;


namespace quiz_app_backend.IServices
{
    public interface IQuizService
    {
        Task<IEnumerable<Quiz>> GetQuizzes();
        Task<IEnumerable<Question>> GetQuestions(string quizId);
        string CreateQuiz(CreateQuizDto createQuestionDto, string UserId);
        string CreateQuestion(CreateQuestionDto createQuestionDto);
        string CreateGame(string QuizId, string UserId);


    }
}