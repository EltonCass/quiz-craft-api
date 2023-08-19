// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Models.DTOs;
public record QuizForUpsert
{
    public IList<CategoryForQuiz> Categories { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public int? CreatedByUserId { get; init; }

    public QuizForUpsert(
        IEnumerable<CategoryForQuiz> categories,
        string description,
        string title,
        int? createdBy = null)
    {
        Categories = new List<CategoryForQuiz>(categories);
        Description = description;
        Title = title;
        CreatedByUserId = createdBy;
    }
}
