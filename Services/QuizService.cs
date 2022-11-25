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
        var data = _context.Quizzes;
        IEnumerable<Quiz> quizzes = from Quiz in data select Quiz;
        return (Task<IEnumerable<Quiz>>)quizzes;
    }

    public Task<IEnumerable<Question>> GetQuestions(GetByIdDto getByIdDto)
    {
        var data = _context.Questions;
        IEnumerable<Question> quizzes = from Question in data where Question.Id == getByIdDto.Id select Question;
        return (Task<IEnumerable<Question>>)quizzes;
    }

    public Task<IEnumerable<Answer>> GetAnswers(GetByIdDto getByIdDto)
    {
        var data = _context.Questions;
        IEnumerable<Answer> answers = (IEnumerable<Answer>)(from Answer in data where Answer.Id == getByIdDto.Id select Answer);
        return (Task<IEnumerable<Answer>>)answers;
    }

}