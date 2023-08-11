// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Models.Entities;

public class Quiz
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? Score { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public ICollection<CategoriesQuiz> CategoriesQuizzes { get; set; } =
        new List<CategoriesQuiz>();
    public User? CreatedByUser { get; set; }
    public User? UpdatedByUser { get; set; }
    public ICollection<Question> Questions { get; set; } =
        new List<Question>();
}