namespace quiz_app_backend.Dtos
{
    public class CreateQuestionDto
    {
        public string Title { get; set; }
        public string QuizId { get; set; }

        public IEnumerable<CreateAnswerDto> createAnswerDtos { get; set; }
    }
}
