// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Models.Entities;

public class FillInBlankQuestion : IEntity
{
    public FillInBlankQuestion()
    {
    }

    public int Id { get; set; }
    public int WordPosition { get; set; }
    public int QuestionId { get; set; }
    public Question? Question { get; set; }
}
