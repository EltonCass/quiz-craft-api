using System;
using System.Collections.Generic;

namespace QuizCraft.Persistence.Models;

public partial class Quiz
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int? Score { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual ICollection<CategoriesQuiz> CategoriesQuizzes { get; set; } = new List<CategoriesQuiz>();

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual User? UpdatedByNavigation { get; set; }
}
