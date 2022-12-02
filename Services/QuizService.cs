using System.Security;
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
    public Task<IEnumerable<Quiz>> GetQuizzes()
    {
        IEnumerable<Quiz> quizzes = from Quiz in _context.Quizzes select Quiz;
        return Task.FromResult(quizzes) ;
    }

    public Task<IEnumerable<Quiz>> GetMyQuizzes(string UserId)
    {
        IEnumerable<Quiz> quizzes = from Quiz in _context.Quizzes where Quiz.UserId == UserId select Quiz;
        return Task.FromResult(quizzes) ;
    }

    public Task<IEnumerable<string>> GetQuestionIds(string QuizId)
    {
        IEnumerable<string> questions = from Question in _context.Questions where Question.QuizId == QuizId select Question.Id;
        return Task.FromResult(questions) ;
    }

    public Task<Question> GetQuestion(string QuestionId)
    {
        var question = _context.Questions
                    .Where(b => b.Id == QuestionId).Include(b => b.Answers)
                    .FirstOrDefault();

        return Task.FromResult(question);
    }

    public Task<string> CreateQuiz(CreateQuizDto createQuestionDto, string UserId)
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

        return Task.FromResult(quiz.Id);
    }

    public Task<string> CreateQuestion(CreateQuestionDto createQuestionDto)
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

        return Task.FromResult(question.Id);
    }

    public Task<string> CreateGame(string QuizId, string UserId)
    {
        var _game = new Game
        {
            Id = Nanoid.Nanoid.Generate(),
            QuizId = QuizId,
            UserID = UserId
        };

        _context.Add(_game);
        _context.SaveChanges();

        return Task.FromResult(_game.Id);
    }

    public Task<string> SubmitAnswer(SubmitAnswerDto submitAnswerDto, string UserId)
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
                        return Task.FromResult("Correct Answer");

                    }
                    else
                    {
                        return Task.FromResult ("Not Correct Answer");
                    }
                }
                else return Task.FromResult ("Answer does not exist");


            }
            return Task.FromResult ("Not your game");

        }
        else return Task.FromResult ("Game does not exist");


    }

    public Task<FinishGameDto> FinishGame(string GameId, string UserId)
    {
        var game = _context.Games
                    .Where(b => b.Id == GameId)
                    .FirstOrDefault();

        
        var quiz = _context.Quizzes
                .Where(b => b.Id == game.QuizId)
                .FirstOrDefault();

        IEnumerable<Score> scores = from Score in _context.Scores where Score.GameId == GameId || Score.AnswerCorrect == true select Score;

        IEnumerable<string> questions = from Question in _context.Questions where Question.QuizId == quiz.Id select Question.Id;

        var finishGameDto = new FinishGameDto
        {
            YourScore = scores.Count(),
            TotalQuestions = questions.Count()
        };

        //Todo: Set game to finished
        return Task.FromResult(finishGameDto);

    }

}