// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Models.Entities;

public class User : IEntity
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public ICollection<Quiz>? QuizzesCreatedBy { get; set; }
    public ICollection<Quiz>? QuizzesUpdatedBy { get; set; }
}
