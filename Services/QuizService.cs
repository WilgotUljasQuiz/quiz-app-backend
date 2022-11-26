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

    public Task<IEnumerable<Question>> GetQuestions(SendStringDto getByIdDto)
    {
        IEnumerable<Question> quizzes = from Question in  _context.Questions where Question.Id == getByIdDto.String select Question;
        return (Task<IEnumerable<Question>>)quizzes;
    }

    public Task<IEnumerable<Answer>> GetAnswers(SendStringDto getByIdDto)
    {
        IEnumerable<Answer> answers = from Answer in _context.Answers where Answer.Id == getByIdDto.String select Answer;
        return (Task<IEnumerable<Answer>>)answers;
    }

    public string CreateQuiz(SendStringDto getByIdDto, string userId)
    {
        var quiz = new Quiz
        {
            Id = Nanoid.Nanoid.Generate(),
            QuizName = getByIdDto.String,
            UserId = userId
        };
        _context.Quizzes.Add(quiz);

        return quiz.Id;
    }

    public string CreateQuestion(CreateQuestionDto createQuestionDto)
    {
        var question = new Question
        {
            Id = Nanoid.Nanoid.Generate(),
            Title = createQuestionDto.Title,
            QuizId= createQuestionDto.QuizId,
        };

        foreach (var item in createQuestionDto.createAnswerDtos)
        {
            var answer = new Answer
            {
                Id = Nanoid.Nanoid.Generate(),
                Title = item.Title,
                IsCorrect = item.IsCorrect,
                questionId = item.QuestionId,
            };
            question.Answers.Add(answer);
        }

        _context.Questions.Add(question);
       
        return question.Id;
    }

  
}