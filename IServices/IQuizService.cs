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
        Task<IEnumerable<Question>> GetQuestions(SendStringDto getByIdDto);
        Task<IEnumerable<Answer>> GetAnswers(SendStringDto getByIdDto);
        string CreateQuiz(SendStringDto getByIdDto, string userId);
        string CreateQuestion(CreateQuestionDto createQuestionDto);

    }
}