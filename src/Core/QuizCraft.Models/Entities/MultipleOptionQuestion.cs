// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Models.Entities;

public class MultipleOptionQuestion: IEntity
{
    public MultipleOptionQuestion()
    {
        Options = new List<Option>();
    }

    public int Id { get; set; }
    public int QuestionId { get; set; }
    public ICollection<Option> Options { get; set; }
    public Question? Question { get; set; }
}
