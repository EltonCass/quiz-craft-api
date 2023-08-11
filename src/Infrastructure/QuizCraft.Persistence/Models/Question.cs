using System;
using System.Collections.Generic;

namespace QuizCraft.Persistence.Models;

public partial class Question
{
    public int Id { get; set; }

    public int? QuizId { get; set; }

    public string? Text { get; set; }

    public string? CorrectAnswer { get; set; }

    public short? Score { get; set; }

    public short? PlacementOrder { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<FillInBlankQuestion> FillInBlankQuestions { get; set; } = new List<FillInBlankQuestion>();

    public virtual ICollection<MultipleOptionQuestion> MultipleOptionQuestions { get; set; } = new List<MultipleOptionQuestion>();

    public virtual Quiz? Quiz { get; set; }
}
