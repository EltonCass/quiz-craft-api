using System;
using System.Collections.Generic;

namespace QuizCraft.Persistence.Models;

public partial class FillInBlankQuestion
{
    public int Id { get; set; }

    public int? QuestionId { get; set; }

    public short? WordPosition { get; set; }

    public virtual Question? Question { get; set; }
}
