namespace QuizCraft.Models.DTOs
{
    public record QuizDTO
    {
        public int Id { get; init; }
        public IList<CategoryDTO> Categories { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public IList<QuestionDTO> Questions { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
        public int? CreatedBy { get; init; }

        public QuizDTO(
            int id,
            IEnumerable<CategoryDTO> categories,
            string description,
            string title,
            IEnumerable<QuestionDTO> questions,
            DateTime createdAt,
            DateTime? updatedAt = null,
            int? createdBy = null)
        {
            Id = id;
            Categories = new List<CategoryDTO>(categories);
            Description = description;
            Title = title;
            Questions = new List<QuestionDTO>(questions);
            CreatedBy = createdBy;
            UpdatedAt = updatedAt;
            CreatedAt = createdAt;
        }
    }
}
