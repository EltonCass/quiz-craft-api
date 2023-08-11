// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Models.Entities;

public class CategoriesQuiz
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int QuizId { get; set; }
    public Category? Category { get; set; }
    public Quiz? Quiz { get; set; }
}