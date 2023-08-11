using System;
using System.Collections.Generic;

namespace QuizCraft.Persistence.Models;

public partial class CategoriesQuiz
{
    public int Id { get; set; }

    public int? CategoryId { get; set; }

    public int? QuizId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Quiz? Quiz { get; set; }
}
