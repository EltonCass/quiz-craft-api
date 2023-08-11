using System;
using System.Collections.Generic;

namespace QuizCraft.Persistence.Models;

public partial class Option
{
    public int Id { get; set; }

    public int? MultipleOptionQuestionId { get; set; }

    public string? Text { get; set; }

    public virtual MultipleOptionQuestion? MultipleOptionQuestion { get; set; }
}
