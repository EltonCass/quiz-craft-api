using System;
using System.Collections.Generic;

namespace QuizCraft.Persistence.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Email { get; set; }

    public string? FullName { get; set; }

    public virtual ICollection<Quiz> QuizCreatedByNavigations { get; set; } = new List<Quiz>();

    public virtual ICollection<Quiz> QuizUpdatedByNavigations { get; set; } = new List<Quiz>();
}
