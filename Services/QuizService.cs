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
    public IEnumerable<Quiz> GetQuizzes()
    {
        IEnumerable<Quiz> quizzes = from Quiz in _context.Quizzes select Quiz;
        return quizzes;
    }

    public IEnumerable<string> GetQuestionIds(string QuizId)
    {
        IEnumerable<string> quizzes = from Question in  _context.Questions where Question.Id == QuizId select Question.Id;
        return quizzes;
    }



    public string CreateQuiz(CreateQuizDto createQuestionDto, string UserId)
    {
        var quiz = new Quiz
        {
            Id = Nanoid.Nanoid.Generate(),
            QuizName = createQuestionDto.QuizName,
            UserId = UserId
        };
        _context.Quizzes.Add(quiz);
        _context.SaveChanges();
        _context.SaveChanges();

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
            question.Answers = new List<Answer> { answer };
        }

        _context.Questions.Add(question);
        _context.SaveChanges();
       
        return question.Id;
    }

    public string CreateGame(string QuizId, string UserId)
    {
        var _game = new Game{
            Id = Nanoid.Nanoid.Generate(),
            QuizId = QuizId,
            UserID = UserId
        };

        _context.Add(_game);
        _context.SaveChanges();
        
        return _game.Id;
    }
}