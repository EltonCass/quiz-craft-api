// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Models.Entities;

public class FillInBlankQuestion : Question
{
    public int WordPosition { get; init; }
    public int QuestionId { get; set; }
    public Question Question { get; set; } = new Question();
}
