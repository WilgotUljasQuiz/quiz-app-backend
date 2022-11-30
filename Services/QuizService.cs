using quiz_app_backend.IServices;
using quiz_app_backend.Models;
using quiz_app_backend.Dtos;
using Microsoft.EntityFrameworkCore;

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
        IEnumerable<string> quizzes = from Question in _context.Questions where Question.QuizId == QuizId select Question.Id;
        return quizzes;
    }

    public Question GetQuestion(string QuestionId)
    {
        var question = _context.Questions
                    .Where(b => b.Id == QuestionId).Include(b => b.Answers)
                    .FirstOrDefault();

        return question;
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
            QuizId = createQuestionDto.QuizId,
            Answers = new List<Answer>()
        };

        foreach (var item in createQuestionDto.createAnswerDtos)
        {
            var answer = new Answer
            {
                Id = Nanoid.Nanoid.Generate(),
                Title = item.Title,
                IsCorrect = item.IsCorrect,
                questionId = question.Id,
            };
            question.Answers.Add(answer);
        }

        _context.Questions.Add(question);
        _context.SaveChanges();

        return question.Id;
    }

    public string CreateGame(string QuizId, string UserId)
    {
        var _game = new Game
        {
            Id = Nanoid.Nanoid.Generate(),
            QuizId = QuizId,
            UserID = UserId
        };

        _context.Add(_game);
        _context.SaveChanges();

        return _game.Id;
    }

    public string SubmitAnswer(SubmitAnswerDto submitAnswerDto, string UserId)
    {
        bool AnswerCorrect = false;

        var game = _context.Games
                    .Where(b => b.Id == submitAnswerDto.GameId)
                    .FirstOrDefault();
        if (game != null)
        {
            if (game.UserID == UserId)
            {

                var answer = _context.Answers.Where(b => b.Id == submitAnswerDto.AnswerId).FirstOrDefault();
                if (answer != null)
                {
                    if (answer.IsCorrect)
                    {
                        AnswerCorrect = true;
                    }

                    var score = new Score
                    {
                        Id = Nanoid.Nanoid.Generate(),
                        AnswerCorrect = AnswerCorrect,
                        GameId = submitAnswerDto.GameId,
                    };
                    _context.Add(score);
                    _context.SaveChanges();
                    if (AnswerCorrect)
                    {
                        return "Correct Answer";

                    }
                    else
                    {
                        return "Not Correct Answer";
                    }
                }
                else return "Answer does not exist";


            }
            return "Not your game";

        }
        else return "Game does not exist";


    }


}