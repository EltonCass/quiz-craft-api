// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Api.Models
{
    public record Quiz
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public IReadOnlyCollection<IQuestion> Questions { get; set; }

        public Quiz(int id, string category, string description, IEnumerable<IQuestion> questions)
        {
            Id = id;
            Category = category;
            Description = description;
            Questions = new List<IQuestion>(questions);
        }
    }
}