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
        IEnumerable<Quiz> GetQuizzes();
        IEnumerable<string> GetQuestionIds(string quizId);
        Question GetQuestion(string questionId);
        string CreateQuiz(CreateQuizDto createQuestionDto, string UserId);
        string CreateQuestion(CreateQuestionDto createQuestionDto);
        string CreateGame(string QuizId, string UserId);


    }
}