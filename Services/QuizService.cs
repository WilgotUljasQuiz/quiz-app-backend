using quiz_app_backend.IServices;
using quiz_app_backend.Models;
using quiz_app_backend.Dtos;

public class QuizService : IQuizService
{

    private readonly IConfiguration _configuration;
    private readonly DataContext _context;

    public QuizService(IConfiguration configuration, DataContext context)
    {
        _configuration = configuration;
        _context = context;
    }
    public Task<IEnumerable<Quiz>> GetQuizzes()
    {
        IEnumerable<Quiz> quizzes = from Quiz in _context.Quizzes select Quiz;
        return (Task<IEnumerable<Quiz>>)quizzes;
    }

    public Task<IEnumerable<Question>> GetQuestions(GetByIdDto getByIdDto)
    {
        IEnumerable<Question> quizzes = from Question in  _context.Questions where Question.Id == getByIdDto.Id select Question;
        return (Task<IEnumerable<Question>>)quizzes;
    }

    public Task<IEnumerable<Answer>> GetAnswers(GetByIdDto getByIdDto)
    {
        IEnumerable<Answer> answers = from Answer in _context.Answers where Answer.Id == getByIdDto.Id select Answer;
        return (Task<IEnumerable<Answer>>)answers;
    }

}