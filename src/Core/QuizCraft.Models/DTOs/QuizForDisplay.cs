// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Models.DTOs;

public record QuizForDisplay
{
    public int Id { get; init; }
    public IList<CategoryForDisplay> Categories { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public IList<QuestionDTO> Questions { get; init; }
    public DateTime? CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public int? CreatedByUserId { get; init; }

    public QuizForDisplay(
        int id,
        IEnumerable<CategoryForDisplay> categories,
        string description,
        string title,
        IEnumerable<QuestionDTO> questions,
        DateTime? createdAt = null,
        DateTime? updatedAt = null,
        int? createdBy = null)
    {
        Id = id;
        Categories = new List<CategoryForDisplay>(categories);
        Description = description;
        Title = title;
        Questions = new List<QuestionDTO>(questions);
        CreatedByUserId = createdBy;
        UpdatedAt = updatedAt;
        CreatedAt = createdAt;
    }
}
