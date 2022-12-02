using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using quiz_app_backend.Models;
using quiz_app_backend.Dtos;
using Microsoft.VisualBasic;


namespace quiz_app_backend.IServices
{
    public interface IQuizService
    {
        Task<IEnumerable<Quiz>> GetQuizzes();
        Task<IEnumerable<Quiz>> GetMyQuizzes(string UserId);
        Task<IEnumerable<string>> GetQuestionIds(string quizId);
        Task<Question> GetQuestion(string questionId);
        Task<string> CreateQuiz(CreateQuizDto createQuestionDto, string UserId);
        Task<string> CreateQuestion(CreateQuestionDto createQuestionDto);
        Task<string> CreateGame(string QuizId, string UserId);
        Task<string> SubmitAnswer(SubmitAnswerDto submitAnswerDto, string UserId); 
        Task<FinishGameDto> FinishGame(string GameId, string UserId); 


    }
}