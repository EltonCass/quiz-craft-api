// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using QuizCraft.Models.Constants;

namespace QuizCraft.Models.Entities;

public class Question : AuditableEntity, IEntity
{
    public Question()
    {
    }

    public int Id { get; set; }
    public int QuizId { get; set; }
    public QuestionType QuestionType { get; set; }
    public string CorrectAnswer { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public int? Score { get; set; }
    public int? PlacementOrder { get; set; }
    public FillInBlankQuestion? FillInBlankQuestion { get; set; }
    public MultipleOptionQuestion? MultipleOptionQuestion { get; set; }
    public Quiz? Quiz { get; set; }
}