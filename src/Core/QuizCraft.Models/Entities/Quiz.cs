// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Models.Entities;

public record Quiz
{
    public int Id { get; init; }
    public IList<Category> Categories { get; init; }
    public string Description { get; init; }
    public IList<BaseQuestion> Questions { get; init; }

    public Quiz(
        int id,
        IEnumerable<Category> categories,
        string description,
        IEnumerable<BaseQuestion> questions)
    {
        Id = id;
        Categories = new List<Category>(categories);
        Description = description;
        Questions = new List<BaseQuestion>(questions);
    }
}