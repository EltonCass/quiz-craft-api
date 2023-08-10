// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Models.DTOs;

public record Quiz
{
    public int Id { get; init; }
    public IList<Category> Categories { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public IList<BaseQuestion> Questions { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public int? CreatedBy { get; init; }

    public Quiz(
        int id,
        IEnumerable<Category> categories,
        string description,
        string title,
        IEnumerable<BaseQuestion> questions,
        DateTime createdAt,
        DateTime? updatedAt = null,
        int? createdBy = null)
    {
        Id = id;
        Categories = new List<Category>(categories);
        Description = description;
        Title = title;
        Questions = new List<BaseQuestion>(questions);
        CreatedBy = createdBy;
        UpdatedAt = updatedAt;
        CreatedAt = createdAt;
    }
}