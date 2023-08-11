// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Models.Entities;

public class MultipleOptionQuestion : Question
{
    public int QuestionId { get; set; }
    public ICollection<Option> Options { get; } = new List<Option>();
    public Question Question { get; set; } = new Question();
}
