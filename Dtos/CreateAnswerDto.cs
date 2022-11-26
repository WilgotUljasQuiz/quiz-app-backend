namespace quiz_app_backend.Dtos
{
    public class CreateAnswerDto
    {
        public string Title { get; set; }
        public bool IsCorrect { get; set; }
        public string QuestionId { get; set; }
    }
}
