// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Models.Entities;

public class Question
{
    public Question()
    {
        Quiz = new Quiz();
    }

    public int Id { get; set; }
    public int QuizId { get; set; }
    public string CorrectAnswer { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public int? Score { get; set; }
    public int? PlacementOrder { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public FillInBlankQuestion? FillInBlankQuestion { get; set; }
    public MultipleOptionQuestion? MultipleOptionQuestion { get; set; }
    public Quiz Quiz { get; set; }
}