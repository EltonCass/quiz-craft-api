using System;
using System.Collections.Generic;

namespace QuizCraft.Persistence.Models;

public partial class MultipleOptionQuestion
{
    public int Id { get; set; }

    public int? QuestionId { get; set; }

    public virtual ICollection<Option> Options { get; set; } = new List<Option>();

    public virtual Question? Question { get; set; }
}
